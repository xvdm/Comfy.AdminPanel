using Amazon.S3;
using Amazon.S3.Model;

namespace AdminPanel.Services.Images.Remove;

public sealed class RemoveImageFromAwsBucketService : IRemoveImageFromFileSystemService
{
    private readonly IAmazonS3 _amazonS3;
    private readonly string _bucketName;

    public RemoveImageFromAwsBucketService(IConfiguration configuration, IAmazonS3 amazonS3)
    {
        _amazonS3 = amazonS3;
        _bucketName = configuration["AWS:S3:BucketName"];
    }

    public async Task RemoveAsync(string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl)) return;
        var request = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = imageUrl.Substring(imageUrl.LastIndexOf('/') + 1)
        };
        await _amazonS3.DeleteObjectAsync(request);
    }

    public async Task RemoveRangeAsync(IEnumerable<string> imageUrls)
    {
        var objects = imageUrls.Select(x => new KeyVersion { Key = x.Substring(x.LastIndexOf('/') + 1) }).ToList();
        var request = new DeleteObjectsRequest
        {
            BucketName = _bucketName,
            Objects = objects
        };
        await _amazonS3.DeleteObjectsAsync(request);
    }
}