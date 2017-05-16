namespace brief.Data
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Library.Transformers;
    using Tesseract;

    public class TesseractTransformer : ITransformer<string, string>
    {
        private readonly string _dataPath;
        private readonly string _mode;

        public TesseractTransformer(string dataPath, string mode)
        {
            _dataPath = dataPath;
            _mode = mode;
        }

        public string Trasform(string source, params object[] configurations)
        {
            //var testImagePath = "./phototest.tif";

            string result = string.Empty;

            try
            {
                //using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
                using (var engine = new TesseractEngine(_dataPath, "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(source))
                    {
                        using (var page = engine.Process(img))
                        {
                            var text = page.GetText();
                            Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());

                            Console.WriteLine("Text (GetText): \r\n{0}", text);
                            Console.WriteLine("Text (iterator):");
                            using (var iter = page.GetIterator())
                            {
                                iter.Begin();

                                do
                                {
                                    do
                                    {
                                        do
                                        {
                                            do
                                            {
                                                if (iter.IsAtBeginningOf(PageIteratorLevel.Block))
                                                {
                                                    //Console.WriteLine("<BLOCK>");
                                                    result += "<BLOCK>";
                                                }

                                                //Console.Write(iter.GetText(PageIteratorLevel.Word));
                                                //Console.Write(" ");
                                                result += iter.GetText(PageIteratorLevel.Word);
                                                result += " ";

                                                if (iter.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word))
                                                {
                                                    //Console.WriteLine();
                                                    result += Environment.NewLine;
                                                }
                                            } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                                            if (iter.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
                                            {
                                                //Console.WriteLine();
                                                result += Environment.NewLine;
                                            }
                                        } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                                    } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                                } while (iter.Next(PageIteratorLevel.Block));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                throw;
                //Console.WriteLine("Unexpected Error: " + e.Message);
                //Console.WriteLine("Details: ");
                //Console.WriteLine(e.ToString());
            }

            return result;
        }

        public Task<string> TransformAsync(string source, params object[] configurations)
            => Task.Run(() => Trasform(source, configurations));
    }
}
