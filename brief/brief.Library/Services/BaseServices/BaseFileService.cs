namespace brief.Library.Services.BaseServices
{
    using System.IO;

    public abstract class BaseFileService
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
