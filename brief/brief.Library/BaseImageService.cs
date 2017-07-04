namespace brief.Library
{
    using System.IO;

    public abstract class BaseImageService
    {
        public virtual void CleanUp(string imagePath)
        {
            File.Delete(imagePath);
        }
    }
}
