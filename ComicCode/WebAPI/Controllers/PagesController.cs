using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto.Page;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly Prn231ComicCodeContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public PagesController(Prn231ComicCodeContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> AddPages([FromForm] PageAddingDto pageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string chapterFolder = Path.Combine("Resources", "Images", pageDto.ChapterId.ToString(), "Page");
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), chapterFolder);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            List<Page> pages = new();

            for (int i = 0; i < pageDto.PageImages.Count; i++)
            {
                var file = pageDto.PageImages[i];
                int pageNumber = i + 1; // Set số trang dựa theo thứ tự upload

                string fileName = $"{pageNumber}{Path.GetExtension(file.FileName)}";
                string filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var page = new Page
                {
                    ChapterId = pageDto.ChapterId,
                    PageNumber = pageNumber,
                    PageImage = $"/{chapterFolder}/{fileName}".Replace("\\", "/")
                };

                pages.Add(page);
            }

            _context.Pages.AddRange(pages);
            await _context.SaveChangesAsync();

            return Ok(pages);
        }
    }
}
