namespace brief.Controllers.Models
{
    using System;

    public class AuthorModel
    {
        public Guid? Id { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorSecondName { get; set; }
        public string AuthorLastName { get; set; }
    }
}
