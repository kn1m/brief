namespace brief.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Extensions;
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
        public async Task<HttpResponseMessage> Create([FromBody] BookModel book)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Forced");
            var isForced = Convert.ToBoolean(headerValues?.FirstOrDefault());

            var result = await _bookService.CreateBook(book);

            return result.CreateRespose(Request, HttpStatusCode.Created, HttpStatusCode.BadRequest);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update([FromBody] BookModel book)
        {
            var result = await _bookService.UpdateBook(book);

            return result.CreateRespose(Request, HttpStatusCode.OK, HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete([FromUri] Guid id)
        {
            var result = await _bookService.RemoveBook(id);

            return result.CreateRespose(Request, HttpStatusCode.OK, HttpStatusCode.NoContent);
        }
    }
}