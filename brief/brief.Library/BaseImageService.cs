namespace brief.Library
{
    using System.IO;

    public abstract class BaseImageService
    {
        public virtual bool TryCleanUp(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
                return true;
            }
            return false;
        }
    }
}
