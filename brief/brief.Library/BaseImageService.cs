namespace brief.Library
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using Controllers.Models;
    using Helpers;

    public abstract class BaseImageService
    {
        private readonly string[] _allowed;

        protected BaseImageService(BaseTransformerSettings settings)
        {
            _allowed = settings.AllowedFormats;
        }

        public virtual string ConvertToAppropirateFormat(string existingFilePath, bool deleteOriginal)
        {
            var image = Image.FromFile(existingFilePath);

            if (image.RawFormat.Equals(ImageFormat.Tiff))
            {
                return existingFilePath;
            }

            var newPath = existingFilePath.Substring(0, existingFilePath.LastIndexOf(".", StringComparison.Ordinal)) + "." +
                          ImageFormat.Tiff;

            image.Save(newPath, ImageFormat.Tiff);
            image.Dispose();

            if (deleteOriginal)
            {
                File.Delete(existingFilePath);
            }

            return newPath;
        }

        //public virtual string SaveImage(ImageModel image)
        //{
        //    if (
        //        _allowed.Contains(
        //            image.Name.Substring(image.Name.LastIndexOf(".", StringComparison.Ordinal), image.Name.Length - 1).ToLower()))
        //    {
        //        var fileSavePath = Path.Combine(_saveImagePath, image.Name);

        //        if (File.Exists(fileSavePath))
        //        {
        //            fileSavePath = Path.Combine(_saveImagePath, Guid.NewGuid() + image.Name);
        //        }

        //        //using (var bw = new BinaryWriter(File.Open(fileSavePath, FileMode.CreateNew)))
        //        //{
        //        //    bw.Write(image.Data);
        //        //    bw.Close();
        //        //}

        //        var fs = new BinaryWriter(new FileStream(fileSavePath, FileMode.Append, FileAccess.Write));
        //        fs.Write(image.Data);
        //        fs.Close();

        //        return fileSavePath;
        //    }
        //    return null;
        //}
    }
}
