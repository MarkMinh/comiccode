using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Dto.Comic;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicsController : ControllerBase
    {
        private readonly Prn231ComicCodeContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ComicsController(Prn231ComicCodeContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComicById(int id)
        {
            var comic = await _context.Comics
                .Include(c => c.Author)
                .FirstOrDefaultAsync(c => c.ComicId == id);

            if (comic == null)
            {
                return NotFound($"Comic with ID {id} not found.");
            }

            return Ok(comic);
        }

        [HttpPost]
        public async Task<IActionResult> AddComic([FromForm] ComicAddingDto comicDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Tạo comic object từ DTO
            var comic = _mapper.Map<Comic>(comicDto);
            comic.CreateAt = DateTime.UtcNow;
            comic.IsActive = true;

            _context.Comics.Add(comic);
            await _context.SaveChangesAsync(); // Lưu trước để lấy ComicId

            // Tạo thư mục lưu trữ dựa theo ComicId
            string comicFolder = Path.Combine("Images", comic.ComicId.ToString());
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", comicFolder);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Lưu ảnh nếu có
            if (comicDto.CoverImage != null)
            {
                var coverImage = comicDto.CoverImage;
                string fileName = $"coverImage{Path.GetExtension(coverImage.FileName)}";
                string filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await coverImage.CopyToAsync(stream);
                }

                // Chuẩn hóa đường dẫn thành dạng URL hợp lệ
                comic.CoverImage = $"/Resources/{comicFolder}/{fileName}".Replace("\\", "/");
                _context.Update(comic);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetComicById), new { id = comic.ComicId }, comic);
        }




    }
}
