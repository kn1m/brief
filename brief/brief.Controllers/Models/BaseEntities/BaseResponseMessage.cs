namespace brief.Controllers.Models.BaseEntities
{
    using System;

    public class BaseResponseMessage : IRecognizable
    {
        public Guid? Id { get; set; }
        public string RawData { get; set; }
    }
}
