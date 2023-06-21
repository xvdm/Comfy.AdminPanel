using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Services.Images.Remove;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Banners;

public sealed record DeleteBannerCommand(int Id) : IRequest;


public sealed class DeleteBannerCommandHandler : IRequestHandler<DeleteBannerCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IRemoveImageFromFileSystemService _removeImageFromFileSystemService;
    private readonly IPublisher _publisher;

    public DeleteBannerCommandHandler(ApplicationDbContext context, IRemoveImageFromFileSystemService removeImageFromFileSystemService, IPublisher publisher)
    {
        _context = context;
        _removeImageFromFileSystemService = removeImageFromFileSystemService;
        _publisher = publisher;
    }

    public async Task Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _context.Banners
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (banner is null) return;

        await _removeImageFromFileSystemService.RemoveAsync(banner.ImageUrl);

        _context.Banners.Remove(banner);
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new BannersInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);
    }
}