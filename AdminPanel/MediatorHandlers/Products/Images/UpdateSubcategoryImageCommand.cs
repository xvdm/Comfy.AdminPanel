using AdminPanel.Data;
using AdminPanel.Models;
using AdminPanel.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Images;

public record UpdateSubcategoryImageCommand(int CategoryId, IFormFile ImageFile) : IRequest;


public class UpdateSubcategoryImageCommandHandler : IRequestHandler<UpdateSubcategoryImageCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IRemoveImageFromFileSystemService _removeImageFromFileSystemService;
    private readonly IUploadImageToFileSystemService _uploadImageToFileSystemService;

    public UpdateSubcategoryImageCommandHandler(ApplicationDbContext context, IRemoveImageFromFileSystemService removeImageFromFileSystemService, IUploadImageToFileSystemService uploadImageToFileSystemService)
    {
        _context = context;
        _removeImageFromFileSystemService = removeImageFromFileSystemService;
        _uploadImageToFileSystemService = uploadImageToFileSystemService;
    }

    public async Task Handle(UpdateSubcategoryImageCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Subcategories.FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);
        if (category is null) throw new HttpRequestException($"No category with id {request.CategoryId}");

        var path = await _uploadImageToFileSystemService.UploadImage(request.ImageFile);
        var image = new SubcategoryImage();
        image.Url = path;

        _removeImageFromFileSystemService.RemoveImage(category.Image.Url);
        _context.SubcategoryImages.Remove(category.Image);

        await _context.SubcategoryImages.AddAsync(image, cancellationToken); // ?? - нужна ли эта строка
        category.Image = image;

        await _context.SaveChangesAsync(cancellationToken);
    }
}