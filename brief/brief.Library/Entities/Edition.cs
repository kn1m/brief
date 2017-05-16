namespace brief.Library.Entities
{
    using System;

    public class Edition
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int Amount { get; set; }
        public virtual Book Book { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}
