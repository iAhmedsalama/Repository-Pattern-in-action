using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Core.Consts;
using RepositoryPattern.Core.Interfaces;
using RepositoryPattern.Core.Models;

namespace RepositoryPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBaseRepository<Book> _booksRepository;

        public BooksController(IBaseRepository<Book> booksRepository)
        {
            _booksRepository = booksRepository;
        }

        [HttpGet]
        public IActionResult GetById()
        {
            return Ok(_booksRepository.GetById(3));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _booksRepository.GetAllAsync());
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName()
        {
            return Ok(await _booksRepository.FindAsync(b=>b.Title =="Book1", new [] {"Author"}));
        }

        [HttpGet("GetAllWithAuthors")]
        public async Task<IActionResult> GetAllWithAuthors()
        {
            return Ok(await _booksRepository.FindAllAsync(b => b.Title.Contains("Book") , new[] { "Author" }));
        }

        [HttpGet("GetOrdered")]
        public async Task<IActionResult> GetOrdered()
        {
            return Ok(await _booksRepository.FindAllAsync(b => b.Title.Contains("Book"), null, null, b=>b.Id, OrderBy.Descending));
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook()
        {
            return Ok(await _booksRepository.Add(new Book { Title="Book4", AuthorId= 2}));
        }
    }
}
