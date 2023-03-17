using AdminPanel.Data;
using AdminPanel.Models;
using AdminPanel.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Images
{
    public class UpdateMainCategoryImageCommand : IRequest
    {
        public int CategoryId { get; set; }
        public IFormFile ImageFile { get; set; }
        public UpdateMainCategoryImageCommand(int categoryId, IFormFile imageFile)
        {
            CategoryId = categoryId;
            ImageFile = imageFile;
        }
    }

    public class UpdateMainCategoryImageCommandHandler : IRequestHandler<UpdateMainCategoryImageCommand>
    {
        private readonly ApplicationDbContext _context;
        private readonly IRemoveImageFromFileSystemService _removeImageFromFileSystemService;
        private readonly IUploadImageToFileSystemService _uploadImageToFileSystemService;

        public UpdateMainCategoryImageCommandHandler(ApplicationDbContext context, IRemoveImageFromFileSystemService removeImageFromFileSystemService, IUploadImageToFileSystemService uploadImageToFileSystemService)
        {
            _context = context;
            _removeImageFromFileSystemService = removeImageFromFileSystemService;
            _uploadImageToFileSystemService = uploadImageToFileSystemService;
        }

        public async Task Handle(UpdateMainCategoryImageCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.MainCategories.FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);
            if (category is null) throw new HttpRequestException($"No category with id {request.CategoryId}");

            var path = await _uploadImageToFileSystemService.UploadImage(request.ImageFile);
            var image = new MainCategoryImage();
            image.Url = path;

            _removeImageFromFileSystemService.RemoveImage(category.Image.Url);
            _context.MainCategoryImages.Remove(category.Image);

            await _context.MainCategoryImages.AddAsync(image, cancellationToken); // ?? - нужна ли эта строка
            category.Image = image;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
