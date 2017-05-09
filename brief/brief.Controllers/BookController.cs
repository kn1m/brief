namespace brief.Controllers
{
    using System;
    using System.Web.Http;
    using Models;
    using Providers;

    public class BookController : ApiController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            if (bookService == null)
            {
                throw new ArgumentNullException(nameof(bookService));
            }

            _bookService = bookService;
        }

        public BookModel Get(BookModel book)
        {
            return null;
        }
    }
}