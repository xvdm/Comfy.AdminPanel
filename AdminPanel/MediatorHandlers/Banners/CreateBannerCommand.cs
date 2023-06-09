using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Models.Entities;
using AdminPanel.Services.Images.Upload;
using MediatR;

namespace AdminPanel.MediatorHandlers.Banners;

public sealed record CreateBannerCommand(string Name, string PageUrl, IFormFile Image) : IRequest;


public sealed class CreateBannerCommandHandler : IRequestHandler<CreateBannerCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUploadImageToFileSystemService _uploadImageToFileSystemService;
    private readonly IPublisher _publisher;

    public CreateBannerCommandHandler(ApplicationDbContext context, IUploadImageToFileSystemService uploadImageToFileSystemService, IPublisher publisher)
    {
        _context = context;
        _uploadImageToFileSystemService = uploadImageToFileSystemService;
        _publisher = publisher;
    }

    public async Task Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        var path = await _uploadImageToFileSystemService.UploadImageAsync(request.Image);

        var banner = new Banner()
        {
            Name = request.Name,
            ImageUrl = path,
            PageUrl = request.PageUrl
        };
        _context.Banners.Add(banner);
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new BannersInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);
    }
}