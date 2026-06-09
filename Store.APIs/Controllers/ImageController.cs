using Microsoft.AspNetCore.Mvc;
using Store.BLL;
using Store.DAL;

namespace Store.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUploadImageManager _imageManager;
        public ImageController(IWebHostEnvironment webHostEnvironment, IUploadImageManager imageManager)
        {
            _webHostEnvironment = webHostEnvironment;
            _imageManager = imageManager;
           
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadDto dto)
        {
            // Extracting hosting information safely in the controller
            string basePath = _webHostEnvironment.ContentRootPath; ;
            string schema = HttpContext.Request.Scheme;
            string host = HttpContext.Request.Host.Value;

            var result = await _imageManager.UploadAsync(dto, basePath, schema, host);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("remove")]
        public IActionResult DeleteImage([FromQuery] string imageUrl)
        {
            string basePath = _webHostEnvironment.WebRootPath;

            var result = _imageManager.RemoveFile(imageUrl, basePath);

            if (!result.Success)
            {
                return BadRequest(result); // Returns "File not found" or exception detail wrapper
            }

            return Ok(result); // Returns success status
        }
    }
}
