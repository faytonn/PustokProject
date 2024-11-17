using Microsoft.Extensions.Configuration;
using Pustok.BLL.Helpers.Abstractions;
using System.Net;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Pustok.DAL.DataContext.AppSettingModels;
using CloudinaryDotNet.Actions;

namespace Pustok.BLL.Helpers.Implementations;

public class CloudinaryService : ICloudinaryService
{
    private readonly IConfiguration _configuration;
    private readonly CloudinaryOptions _optionsDto;
    private readonly Cloudinary _cloudinary = null!;

    public CloudinaryService(IConfiguration configuration)
    {
        _configuration = configuration;
        _optionsDto = _configuration.GetSection("CloudinarySettings").Get<CloudinaryOptions>() ?? new();

        var myAccount = new Account { ApiKey = _optionsDto.APIKey, ApiSecret = _optionsDto.APISecret, Cloud = _optionsDto.CloudName };

        _cloudinary = new Cloudinary(myAccount);
        _cloudinary.Api.Secure = true;
    }

    public async Task<string> FileCreateAsync(IFormFile file)
    {
        string fileName = string.Concat(Guid.NewGuid(), file.FileName.Substring(file.FileName.LastIndexOf('.')));

        var uploadResult = new ImageUploadResult();
        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, stream),
                AssetFolder = "pustok"
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }
        string url = uploadResult.SecureUrl.ToString();

        return url;
    }

    public async Task<bool> FileDeleteAsync(string filePath)
    {
        try
        {
            string publicIdWithExtension = filePath.Substring(filePath.LastIndexOf("pustok"));
            string publicId = publicIdWithExtension.Substring(0, publicIdWithExtension.LastIndexOf('.'));

            var deleteParams = new DelResParams()
            {
                PublicIds = new List<string> { publicId },
                Type = "upload",
                ResourceType = ResourceType.Image
            };
            var result = await _cloudinary.DeleteResourcesAsync(deleteParams);

            return result.StatusCode == HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
    public async Task<string> ImageCreateAsync(IFormFile file)
    {
        if (file == null) throw new ArgumentNullException(nameof(file));

        string fileName = string.Concat(Guid.NewGuid(), file.FileName.Substring(file.FileName.LastIndexOf('.')));
        var myAccount = new Account
        {
            ApiKey = _configuration["CloudinarySettings:APIKey"],
            ApiSecret = _configuration["CloudinarySettings:APISecret"],
            Cloud = _configuration["CloudinarySettings:CloudName"]
        };

        Cloudinary _cloudinary = new Cloudinary(myAccount);
        _cloudinary.Api.Secure = true;
        var uploadResult = new ImageUploadResult();

        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, stream),
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }
        string url = uploadResult.SecureUrl.ToString();

        return url;
    }
}
