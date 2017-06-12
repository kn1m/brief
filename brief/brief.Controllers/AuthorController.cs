namespace brief.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Models;
    using Providers;

    public class AuthorController : ApiController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
        }

        [HttpPost]
        public async Task<AuthorModel> Create([FromBody] AuthorModel author)
            => await _authorService.CreateAuthor(author);

        [HttpPut]
        public async Task<AuthorModel> Update([FromBody] AuthorModel author)
            => await _authorService.UpdateAuthor(author);

        [HttpDelete]
        public async Task Delete([FromUri] Guid id)
            => await _authorService.RemoveAuthor(id);
    }
}
