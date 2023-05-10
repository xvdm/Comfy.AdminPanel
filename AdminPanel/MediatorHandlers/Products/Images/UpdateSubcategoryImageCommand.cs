using AdminPanel.Data;
using AdminPanel.Models;
using AdminPanel.Services.Images.Remove;
using AdminPanel.Services.Images.Upload;
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

        if (category.Image is not null)
        {
            _removeImageFromFileSystemService.RemoveImage(category.Image.Url);
        }

        var path = await _uploadImageToFileSystemService.UploadImage(request.ImageFile);
        var image = new SubcategoryImage
        {
            Url = path
        };

        category.Image = image;
        await _context.SaveChangesAsync(cancellationToken);
    }
}