namespace brief.Library.Entities
{
    using System;
    using System.Collections.Generic;

    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IList<Edition> Editions { get; set; }
        public virtual Series Series { get; set; }
    }
}
