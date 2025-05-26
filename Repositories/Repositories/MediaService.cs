using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories
{
    public class MediaService : IMediaService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MediaService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string SaveMedia(IFormFile file)
        {
            if (file == null || file.Length == 0) throw new FileNotFoundException();
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadPath);
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadPath, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(stream).GetAwaiter();
            }
            return Path.Combine("uploads", uniqueFileName).Replace("\\", "/");
        }
    }
}
