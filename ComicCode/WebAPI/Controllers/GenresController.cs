using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto.Genre;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly Prn231ComicCodeContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public GenresController(Prn231ComicCodeContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("add")]
        public IActionResult AddGenre([FromBody] GenreAddingDto genreDto)
        {
            if (string.IsNullOrWhiteSpace(genreDto.GenreName))
            {
                return BadRequest("Genre name is required.");
            }

            Genre newGenre = new Genre
            {
                GenreName = genreDto.GenreName,
                GenreDescription = genreDto.GenreDescription,
                CreateAt = DateTime.Now,
                IsActive = true

            };
            _context.Genres.Add(newGenre);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetGenreById), new { id = newGenre.GenreId }, newGenre);
        }

        [HttpGet("{id}")]
        public IActionResult GetGenreById(int id)
        {
            var genre = _context.Genres.FirstOrDefault(x => x.GenreId == id && x.IsActive == true);
            if (genre == null) return NotFound("Genre not found.");

            return Ok(genre);
        }


    }
}
