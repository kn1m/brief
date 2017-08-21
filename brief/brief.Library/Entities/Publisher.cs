namespace brief.Library.Entities
{
    using System;
    using System.Collections.Generic;

    public class Publisher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Founded { get; set; }
        public IList<Edition> Editions { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var publisher = obj as Publisher;
            if (publisher != null)
            {
                return publisher.Name == Name && publisher.Description == Description;
            }

            return false;
        }

        public override int GetHashCode()
            => 2108858624 + EqualityComparer<Guid>.Default.GetHashCode(Id);
    }
}
