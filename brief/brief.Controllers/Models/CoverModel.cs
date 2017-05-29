namespace brief.Controllers.Models
{
    using System;

    public class CoverModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public byte[] CoverData { get; set; }
    }
}
