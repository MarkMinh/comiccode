using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Dto.Chapter;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChaptersController : ControllerBase
    {
        private readonly Prn231ComicCodeContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ChaptersController(Prn231ComicCodeContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> AddChapter([FromBody] ChapterAddingDto chapterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chapter = _mapper.Map<Chapter>(chapterDto);
            chapter.CreateAt = DateTime.UtcNow;
            chapter.IsActive = true;

            _context.Chapters.Add(chapter);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChapterById), new { id = chapter.ChapterId }, null);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChapterDto>> GetChapterById(int id)
        {
            var chapter = await _context.Chapters.Include(x => x.Pages).FirstOrDefaultAsync(a => a.ChapterId == id);
            if (chapter == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ChapterDto>(chapter));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChapter(int id, [FromBody] ChapterAddingDto chapterDto)
        {
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter == null)
            {
                return NotFound();
            }

            _mapper.Map(chapterDto, chapter);
            chapter.UpdateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
