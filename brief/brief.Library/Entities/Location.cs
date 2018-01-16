namespace brief.Library.Entities
{
    using System;

    public class Location
    {
        public Guid Id { get; set; }
        public byte Stage { get; set; }
        public byte Loker { get; set; }
        public byte Shelf { get; set; }
        public string Address { get; set; }
        public Guid EditionId { get; set; }
        public virtual Edition Edition { get; set; }
    }
}
