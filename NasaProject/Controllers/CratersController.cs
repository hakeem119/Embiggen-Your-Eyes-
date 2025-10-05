using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NasaProject.DTOs;
using NasaProject.Services;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NasaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CratersController : ControllerBase
    {



        [HttpPost("detect")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(List<CraterDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<List<CraterDTO>>> DetectCrater([FromForm] IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest("No image uploaded.");

            var filePath = Path.Combine(Path.GetTempPath(), image.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var craters = CraterDetection.FindCraters(filePath);
            System.IO.File.Delete(filePath);

            return Ok(craters);
        }




        //public async Task<IActionResult> DetectCrater([FromForm] IFormFile image)
        //{
        //    if (image == null || image.Length == 0)
        //        return BadRequest("No image uploaded.");

        //    // Save temporarily
        //    var filePath = Path.Combine(Path.GetTempPath(), image.FileName);
        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await image.CopyToAsync(stream);
        //    }

        //  //  Detect craters
        //    var craters = CraterDetection.FindCraters(filePath);

        //    // Delete temp file
        //    System.IO.File.Delete(filePath);

        //    // Return JSON
        //    return Ok(craters);
        //    //return Ok(new { Message = "Crater detection not implemented yet." });
        //}

    }
}