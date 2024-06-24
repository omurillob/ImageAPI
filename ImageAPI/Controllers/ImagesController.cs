using Microsoft.AspNetCore.Mvc;
using ImageAPI.DTOs;

namespace ImageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id cannot be null or empty.");
            }

            var url = await _imageService.GetImageById(id);

            return Ok(new ImageDTO(id, url));
        }
    }
}
