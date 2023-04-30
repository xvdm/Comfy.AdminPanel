using AdminPanel.Data;
using AdminPanel.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Images;

public record DeleteProductImageCommand(int ImageId) : IRequest;


public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IRemoveImageFromFileSystemService _removeImageFromFileSystemService;

    public DeleteProductImageCommandHandler(ApplicationDbContext context, IRemoveImageFromFileSystemService removeImageFromFileSystemService)
    {
        _context = context;
        _removeImageFromFileSystemService = removeImageFromFileSystemService;
    }

    public async Task Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
    {
        var image = await _context.Images.FirstAsync(x => x.Id == request.ImageId, cancellationToken);
        if (image is null)
        {
            throw new HttpRequestException("Image was not found");
        }
        _context.Images.Remove(image);
        await _context.SaveChangesAsync(cancellationToken);

        _removeImageFromFileSystemService.RemoveImage(image.Url);
    }
}