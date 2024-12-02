namespace TVStation.Services
{
    public interface IFileUploadService
    {
        string UploadFile(IFormFile file);
    }
}
