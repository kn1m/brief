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

        public virtual string ConvertToAppropirateFormat(string existingFilePath)
        {
            if (
                _allowed.Contains(
                    existingFilePath.Substring(existingFilePath.LastIndexOf(".", StringComparison.Ordinal), existingFilePath.Length)))
            {
                var image = Image.FromFile(existingFilePath);

                if (image.RawFormat.Equals(ImageFormat.Tiff))
                {
                    return existingFilePath;
                }

                var newPath = existingFilePath.Substring(0, existingFilePath.LastIndexOf(".", StringComparison.Ordinal)) +
                              ImageFormat.Tiff;

                image.Save(newPath, ImageFormat.Tiff);
                //File.Delete(f);
            }

            return null;
        }

        public virtual string SaveImage(ImageModel image)
        {
            var fileSavePath = Path.Combine(_saveImagePath, image.Name);

            using (var bw = new BinaryWriter(File.Open(fileSavePath, FileMode.OpenOrCreate)))
            {
                bw.Write(image.Data);
            }

            return fileSavePath;
        }

    }
}
