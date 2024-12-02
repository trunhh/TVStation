using Microsoft.AspNetCore.Hosting;  // Make sure to use this namespace for IWebHostEnvironment
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class MediaController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    // Constructor
    public MediaController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        // Get the wwwroot path from the IWebHostEnvironment
        var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

        // Ensure the directory exists
        Directory.CreateDirectory(uploadPath);

        var filePath = Path.Combine(uploadPath, file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok(new { FilePath = filePath });
    }
}
