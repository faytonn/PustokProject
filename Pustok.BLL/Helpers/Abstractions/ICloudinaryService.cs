namespace Pustok.BLL.Helpers.Abstractions;

public interface ICloudinaryService
{
    Task<string> ImageCreateAsync(IFormFile file);
    Task<bool> FileDeleteAsync(string filePath);
    Task<string> FileCreateAsync(IFormFile file);
}
