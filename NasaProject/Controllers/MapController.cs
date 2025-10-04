using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NasaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly string _tilesPath;

        public MapController(IConfiguration configuration)
        {
            _tilesPath = configuration["MapSettings:TilesOutputPath"];
        }


        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Map API is working!");
        }

        [HttpGet("tile/{z}/{x}/{y}.png")]
        public IActionResult GetTile(int z, int x, int y)
        {
            string tilePath = Path.Combine(_tilesPath, $"{z}", $"{x}_{y}.png");

            if (!System.IO.File.Exists(tilePath))
                return NotFound();

            var image = System.IO.File.ReadAllBytes(tilePath);
            return File(image, "image/png");
        }

    }
}