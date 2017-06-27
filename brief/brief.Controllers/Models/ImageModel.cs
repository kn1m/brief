namespace brief.Controllers.Models
{
    using System;

    public class ImageModel
    {
        public Guid? TargetId { get; set; }
        public string Path { get; set; }
        public string TargetLanguage { get; set; }
    }
}
