namespace brief.Library.Entities
{
    using System;
    using System.Collections.Generic;

    public class Edition
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int Amount { get; set; }
        public Guid BookId { get; set; }
        public virtual Book Book { get; set; }
        public Guid PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual IList<Cover> Covers { get; set; }
        public EditionType EditionType { get; set; }
        public Language Language { get; set; }
    }
}
