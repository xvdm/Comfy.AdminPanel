using Amazon.S3;
using Amazon.S3.Model;

namespace AdminPanel.Services.Images.Remove;

public sealed class RemoveImageFromAwsBucketService : IRemoveImageFromFileSystemService
{
    private readonly IConfiguration _configuration;

    public RemoveImageFromAwsBucketService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task RemoveImage(string imageUrl)
    {
        var accessKey = _configuration["AWS:S3:AccessKey"];
        var secretKey = _configuration["AWS:S3:SecretKey"];
        var bucketName = _configuration["AWS:S3:BucketName"];

        var config = new AmazonS3Config
        {
            ServiceURL = "https://s3.amazonaws.com"
        };
        using var client = new AmazonS3Client(accessKey, secretKey, config);
        var request = new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = imageUrl.Substring(imageUrl.LastIndexOf('/') + 1)
        };
        await client.DeleteObjectAsync(request);
    }
}