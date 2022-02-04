using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Core;
using RepositoryPattern.Core.Consts;
using RepositoryPattern.Core.Interfaces;
using RepositoryPattern.Core.Models;

namespace RepositoryPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetById()
        {
            return Ok(_unitOfWork.Books.GetById(3));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.Books.GetAllAsync());
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName()
        {
            return Ok(await _unitOfWork.Books.FindAsync(b=>b.Title =="Book1", new [] {"Author"}));
        }

        [HttpGet("GetAllWithAuthors")]
        public async Task<IActionResult> GetAllWithAuthors()
        {
            return Ok(await _unitOfWork.Books.FindAllAsync(b => b.Title.Contains("Book") , new[] { "Author" }));
        }

        [HttpGet("GetOrdered")]
        public async Task<IActionResult> GetOrdered()
        {
            return Ok(await _unitOfWork.Books.FindAllAsync(b => b.Title.Contains("Book"), null, null, b=>b.Id, OrderBy.Descending));
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook()
        {
            var book = await _unitOfWork.Books.Add(new Book { Title = "Book5", AuthorId = 2 });
            _unitOfWork.Complete();

            return Ok(book);
        }

        
    }
}
