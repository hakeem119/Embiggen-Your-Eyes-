using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NasaProject.Data;
using NasaProject.Models;
using NasaProject.Services;

namespace NasaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly AppDbContext  _context;
        private readonly IWebHostEnvironment _environment;

        public ImagesController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }




        //[HttpGet("check-map-path")]
        //public IActionResult CheckMapPath()
        //{
        //    return Ok(new { MapPath = _mapService.GetMapPath() });
        //}

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file, int userId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // مسار الحفظ
            var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            // اسم الملف
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsPath, fileName);

            // احفظ الملف على السيرفر
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // سجل في قاعدة البيانات
            var image = new Image
            {
                UserId = userId,
                FileName = fileName,
                FilePath = "/uploads/" + fileName,
                UploadDate = DateTime.Now,
                Status = "Active"
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return Ok(new { image.ImageId, image.FileName, image.FilePath });
        }
    }
}
