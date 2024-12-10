using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Get the wwwroot path from IWebHostEnvironment
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

            // Ensure the uploads directory exists
            Directory.CreateDirectory(uploadPath);

            // Create a unique file name (e.g., using GUID to avoid conflicts)
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadPath, uniqueFileName);

            // Save the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return the relative URL for the uploaded file
            var relativeFilePath = Path.Combine("uploads", uniqueFileName).Replace("\\", "/");
            return Ok(new { FilePath = relativeFilePath });
        }
        catch (Exception ex)
        {
            // Handle any errors and return a 500 status code
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
