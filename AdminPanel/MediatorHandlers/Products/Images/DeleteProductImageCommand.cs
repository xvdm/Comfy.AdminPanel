using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Services.Images.Remove;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Images;

public record DeleteProductImageCommand(int ImageId) : IRequest;


public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IRemoveImageFromFileSystemService _removeImageFromFileSystemService;
    private readonly IPublisher _publisher;

    public DeleteProductImageCommandHandler(ApplicationDbContext context, IRemoveImageFromFileSystemService removeImageFromFileSystemService, IPublisher publisher)
    {
        _context = context;
        _removeImageFromFileSystemService = removeImageFromFileSystemService;
        _publisher = publisher;
    }

    public async Task Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
    {
        var image = await _context.Images
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.ImageId, cancellationToken);
        if (image is null) return;

        _removeImageFromFileSystemService.RemoveImage(image.Url);

        _context.Images.Remove(image);
        await _context.SaveChangesAsync(cancellationToken);

        var productUrl = await _context.Products.Select(x => new
        {
            x.Id,
            x.Url
        }).FirstOrDefaultAsync(x => x.Id == image.ProductId, cancellationToken);

        var notification = new ProductInvalidatedEvent(image.ProductId, productUrl!.Url);
        await _publisher.Publish(notification, cancellationToken);
    }
}