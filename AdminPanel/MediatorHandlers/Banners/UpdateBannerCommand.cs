using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Services.Images.Remove;
using AdminPanel.Services.Images.Upload;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Banners;

public sealed record UpdateBannerCommand(int Id, string Name, string PageUrl, IFormFile? Image) : IRequest;


public sealed class UpdateBannerCommandHandler : IRequestHandler<UpdateBannerCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUploadImageToFileSystemService _uploadImageToFileSystemService;
    private readonly IRemoveImageFromFileSystemService _removeImageFromFileSystemService;
    private readonly IPublisher _publisher;

    public UpdateBannerCommandHandler(ApplicationDbContext context, IUploadImageToFileSystemService uploadImageToFileSystemService, 
        IRemoveImageFromFileSystemService removeImageFromFileSystemService, IPublisher publisher)
    {
        _context = context;
        _uploadImageToFileSystemService = uploadImageToFileSystemService;
        _removeImageFromFileSystemService = removeImageFromFileSystemService;
        _publisher = publisher;
    }

    public async Task Handle(UpdateBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _context.Banners.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (banner is null) return;

        if (request.Image is not null)
        {
            _removeImageFromFileSystemService.Remove(banner.ImageUrl);
            var path = await _uploadImageToFileSystemService.UploadImageAsync(request.Image);
            banner.ImageUrl = path;
        }

        banner.Name = request.Name;
        banner.PageUrl = request.PageUrl;
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new BannersInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);
    }
}