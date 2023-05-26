using Amazon.S3;
using Amazon.S3.Transfer;

namespace AdminPanel.Services.Images.Upload;

public sealed class UploadImageToAwsBucketService : IUploadImageToFileSystemService
{
    private readonly IConfiguration _configuration;

    public UploadImageToAwsBucketService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> UploadImageAsync(IFormFile imageFile)
    {
        var accessKey = _configuration["AWS:S3:AccessKey"];
        var secretKey = _configuration["AWS:S3:SecretKey"];
        var bucketName = _configuration["AWS:S3:BucketName"];
        var guid = Guid.NewGuid();
        var config = new AmazonS3Config
        {
            ServiceURL = "https://s3.amazonaws.com"
        };
        using (var client = new AmazonS3Client(accessKey, secretKey, config))
        {
            using var newMemoryStream = new MemoryStream();

            await imageFile.CopyToAsync(newMemoryStream);

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = newMemoryStream,
                Key = guid.ToString(),
                BucketName = bucketName
            };

            var fileTransferUtility = new TransferUtility(client);
            await fileTransferUtility.UploadAsync(uploadRequest);
        }

        var path = $"https://s3.amazonaws.com/{bucketName}/{guid}";

        return path;
    }
}