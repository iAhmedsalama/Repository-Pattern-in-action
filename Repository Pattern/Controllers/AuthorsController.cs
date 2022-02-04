using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Core;
using RepositoryPattern.Core.Interfaces;
using RepositoryPattern.Core.Models;


namespace RepositoryPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetById()
        {
            return Ok(_unitOfWork.Authors.GetById(1));
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync()
        {
            return  Ok(await _unitOfWork.Authors.GetByIdAsync(1));
        }
    }
}
