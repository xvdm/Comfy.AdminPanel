using Amazon.S3;
using Amazon.S3.Model;

namespace AdminPanel.Services.Images.Upload;

public sealed class UploadImageToAwsBucketService : IUploadImageToFileSystemService
{
    private readonly IConfiguration _configuration;
    private readonly IAmazonS3 _amazonS3;

    public UploadImageToAwsBucketService(IConfiguration configuration, IAmazonS3 amazonS3)
    {
        _configuration = configuration;
        _amazonS3 = amazonS3;
    }

    public async Task<string> UploadImageAsync(IFormFile imageFile)
    {
        var bucketName = _configuration["AWS:S3:BucketName"];
        var guid = Guid.NewGuid();
        
        var request = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = guid.ToString(),
            ContentType = imageFile.ContentType,
            InputStream = imageFile.OpenReadStream()
        };

        await _amazonS3.PutObjectAsync(request);

        var path = $"https://s3.amazonaws.com/{bucketName}/{guid}";
        return path;
    }
}