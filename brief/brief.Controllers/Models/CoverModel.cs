namespace brief.Controllers.Models
{
    using System;
    using BaseEntities;

    public class CoverModel : IRecognizable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public byte[] CoverData { get; set; }
        public string RawData { get; set; }
    }
}
