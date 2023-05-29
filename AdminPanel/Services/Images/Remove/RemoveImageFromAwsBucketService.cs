using Amazon.S3;
using Amazon.S3.Model;

namespace AdminPanel.Services.Images.Remove;

public sealed class RemoveImageFromAwsBucketService : IRemoveImageFromFileSystemService
{
    private readonly string _accessKey;
    private readonly string _secretKey;
    private readonly string _bucketName;
    private const string ServiceUrl = "https://s3.amazonaws.com";

    public RemoveImageFromAwsBucketService(IConfiguration configuration)
    {
        _accessKey = configuration["AWS:S3:AccessKey"];
        _secretKey = configuration["AWS:S3:SecretKey"];
        _bucketName = configuration["AWS:S3:BucketName"];
    }

    public async Task RemoveAsync(string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl)) return;
        var config = new AmazonS3Config { ServiceURL = ServiceUrl };
        using var client = new AmazonS3Client(_accessKey, _secretKey, config);
        var request = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = imageUrl.Substring(imageUrl.LastIndexOf('/') + 1)
        };
        await client.DeleteObjectAsync(request);
    }

    public async Task RemoveRangeAsync(IEnumerable<string> imageUrls)
    {
        var config = new AmazonS3Config { ServiceURL = ServiceUrl };
        using var client = new AmazonS3Client(_accessKey, _secretKey, config);
        var objects = imageUrls.Select(x => new KeyVersion { Key = x.Substring(x.LastIndexOf('/') + 1) }).ToList();
        var request = new DeleteObjectsRequest
        {
            BucketName = _bucketName,
            Objects = objects
        };
        await client.DeleteObjectsAsync(request);
    }
}