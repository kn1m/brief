namespace brief.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Models;
    using Providers;

    public class BookController : ApiController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        [HttpPost]
        public async Task<BookModel> Create([FromBody] BookModel book)
            => await _bookService.CreateBook(book);

        [HttpPut]
        public async Task<BookModel> Update([FromBody] BookModel book)
            => await _bookService.UpdateBook(book);

        [HttpDelete]
        public async Task Delete([FromUri] Guid id)
            => await _bookService.RemoveBook(id);
    }
}