namespace Pustok.BLL.Helpers.Utilities;

public static class FileHelper
{
    public static bool CheckType(this IFormFile file, string type = "image")
    {
        return file.ContentType.Contains(type);
    }
}
