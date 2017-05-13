namespace brief.Library
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;

    public abstract class BaseImageService
    {
        private readonly ImageFormat[] _allowed = { ImageFormat.Tiff };

        public virtual string ConvertToAppropirateFormat(string existingFilePath)
        {   
            //TODO : rework file extension check without loading file && all god damn thing
            var image = Image.FromFile(existingFilePath);

            if (_allowed.Contains(image.RawFormat))
            {
                return existingFilePath;
            }

            var newPath = existingFilePath.LastIndexOf(".", StringComparison.CurrentCulture);

            //png.Save("a.tiff", ImageFormat.Tiff);

            return null;
        }
    }
}
