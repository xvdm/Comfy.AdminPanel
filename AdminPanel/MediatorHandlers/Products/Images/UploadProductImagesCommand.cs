using AdminPanel.Data;
using AdminPanel.Models;
using AdminPanel.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Images
{
    public record UploadProductImagesCommand(int ProductId, ICollection<IFormFile> Files) : IRequest<bool>;


    public class UploadProductImagesCommandHandler : IRequestHandler<UploadProductImagesCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly IUploadImageToFileSystemService _uploadImageToFileSystemService;

        public UploadProductImagesCommandHandler(ApplicationDbContext context, IUploadImageToFileSystemService uploadImageToFileSystemService)
        {
            _context = context;
            _uploadImageToFileSystemService = uploadImageToFileSystemService;
        }

        public async Task<bool> Handle(UploadProductImagesCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

            if (product is null) return false;

            foreach (var file in request.Files)
            {
                if (file.Length <= 0) continue;
                if (product.Images.Count >= 6) return false;

                var path = await _uploadImageToFileSystemService.UploadImage(file);
                var image = new Image
                {
                    ProductId = request.ProductId,
                    Url = path
                };
                _context.Images.Add(image);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return true;
        }
    }
}