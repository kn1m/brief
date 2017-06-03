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
        private readonly string _saveImagePath;

        protected BaseImageService(StorageSettings settings)
        {
            _allowed = settings.AllowedFormats;
            _saveImagePath = settings.StoragePath;
        }

        public virtual string ConvertToAppropirateFormat(string existingFilePath, bool deleteOriginal)
        {
            var image = Image.FromFile(existingFilePath);

            if (image.RawFormat.Equals(ImageFormat.Tiff))
            {
                return existingFilePath;
            }

            var newPath = existingFilePath.Substring(0, existingFilePath.LastIndexOf(".", StringComparison.Ordinal)) +
                          ImageFormat.Tiff;

            image.Save(newPath, ImageFormat.Tiff);

            if (deleteOriginal)
            {
                File.Delete(existingFilePath);
            }

            return newPath;
        }

        public virtual string SaveImage(ImageModel image)
        {
            if (
                _allowed.Contains(
                    image.Name.Substring(image.Name.LastIndexOf(".", StringComparison.Ordinal), image.Name.Length)))
            {
                var fileSavePath = Path.Combine(_saveImagePath, image.Name);

                using (var bw = new BinaryWriter(File.Open(fileSavePath, FileMode.OpenOrCreate)))
                {
                    bw.Write(image.Data);
                }

                return fileSavePath;
            }
            return null;
        }
    }
}
