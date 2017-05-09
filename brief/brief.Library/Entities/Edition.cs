namespace brief.Library.Entities
{
    using System;

    public class Edition
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public virtual Book Book { get; set; }
    }
}
