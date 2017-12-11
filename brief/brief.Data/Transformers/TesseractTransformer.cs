namespace brief.Data.Transformers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Library.Helpers;
    using Library.Transformers;
    using Tesseract;

    public class TesseractTransformer : ITransformer<string, string>
    {
        private readonly string _dataPath;
        private readonly EngineMode _mode;

        public TesseractTransformer(string dataPath, EngineMode mode)
        {
            Guard.AssertNotNull(dataPath, nameof(dataPath));

            _dataPath = dataPath;
            _mode = mode;
        }

        public string Trasform(string source, params object[] configurations)
        {
            string result = string.Empty;

            if (configurations.Length == 0)
            {
                throw new ArgumentNullException(nameof(configurations));
            }

            try
            {
                using (var engine = new TesseractEngine(_dataPath, configurations.First().ToString(), _mode))
                {
                    using (var img = Pix.LoadFromFile(source))
                    {
                        using (var page = engine.Process(img))
                        {
                            var text = page.GetText();
                            Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());

                            result += text;
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
                                                    result += Environment.NewLine;
                                                }

                                                result += iter.GetText(PageIteratorLevel.Word);
                                                result += " ";

                                                if (iter.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word))
                                                {
                                                    result += Environment.NewLine;
                                                }
                                            } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                                            if (iter.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
                                            {
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
                //TODO: to config
                Trace.TraceError(e.ToString());
                throw;
            }

            return result;
        }

        public Task<string> TransformAsync(string source, params object[] configurations)
            => Task.Run(() => Trasform(source, configurations));
    }
}
