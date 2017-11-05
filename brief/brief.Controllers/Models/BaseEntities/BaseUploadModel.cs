namespace brief.Controllers.Models.BaseEntities
{
    using System;

    public class BaseUploadModel
    {
        public Guid? TargetId { get; set; }
        public string Path { get; set; }
    }
}
