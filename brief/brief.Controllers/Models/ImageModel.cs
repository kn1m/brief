namespace brief.Controllers.Models
{
    using System;

    public class ImageModel
    {
        public int Length { get; set; }
        public byte[] Data { get; set; }
        public string Name { get; set; }
        public Guid? EditionId { get; set; }
        public Guid? CoverId { get; set; }
    }
}
