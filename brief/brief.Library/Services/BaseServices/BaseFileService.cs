namespace brief.Library.Services.BaseServices
{
    using System;
    using System.IO.Abstractions;

    public abstract class BaseFileService
    {
        protected readonly IFileSystem FileSystem;

        protected BaseFileService(IFileSystem fileSystem)
        {
            FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public virtual bool TryCleanUp(string imagePath)
        {
            if (FileSystem.File.Exists(imagePath))
            {
                FileSystem.File.Delete(imagePath);
                return true;
            }
            return false;
        }
    }
}
