using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Dto.Author;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly Prn231ComicCodeContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthorsController(Prn231ComicCodeContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorAddingDto>>> GetAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<AuthorAddingDto>>(authors));
        }

        [HttpPost]
        public async Task<ActionResult<AuthorAddingDto>> CreateAuthor(AuthorAddingDto authorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var author = _mapper.Map<Author>(authorDto);
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            var createdAuthorDto = _mapper.Map<AuthorAddingDto>(author);
            return CreatedAtAction(nameof(GetAuthors), new { id = author.AuthorId }, createdAuthorDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorAddingDto>> GetAuthorById(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound(new { message = "Không tìm thấy tác giả với ID này." });
            }

            return Ok(_mapper.Map<AuthorAddingDto>(author));
        }
    }
}
