using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Services.Images.Remove;
using AdminPanel.Services.Images.Upload;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Images;

public sealed record UpdateMainCategoryImageCommand(int CategoryId, IFormFile ImageFile) : IRequest;


public sealed class UpdateMainCategoryImageCommandHandler : IRequestHandler<UpdateMainCategoryImageCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IRemoveImageFromFileSystemService _removeImageFromFileSystemService;
    private readonly IUploadImageToFileSystemService _uploadImageToFileSystemService;
    private readonly IPublisher _publisher;

    public UpdateMainCategoryImageCommandHandler(ApplicationDbContext context, IRemoveImageFromFileSystemService removeImageFromFileSystemService, IUploadImageToFileSystemService uploadImageToFileSystemService, IPublisher publisher)
    {
        _context = context;
        _removeImageFromFileSystemService = removeImageFromFileSystemService;
        _uploadImageToFileSystemService = uploadImageToFileSystemService;
        _publisher = publisher;
    }

    public async Task Handle(UpdateMainCategoryImageCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.MainCategories.FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);
        if (category is null) throw new HttpRequestException($"No category with id {request.CategoryId}");

        var path = await _uploadImageToFileSystemService.UploadImageAsync(request.ImageFile);
        if (string.IsNullOrEmpty(category.ImageUrl) == false)
        {
            await _removeImageFromFileSystemService.RemoveAsync(category.ImageUrl);
        }

        category.ImageUrl = path;
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new CategoriesMenuInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);
    }
}