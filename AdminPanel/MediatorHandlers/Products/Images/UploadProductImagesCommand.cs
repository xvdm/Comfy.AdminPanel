using AdminPanel.Data;
using MediatR;
using AdminPanel.Models;
using AdminPanel.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis;

namespace AdminPanel.MediatorHandlers.Products.Images
{
    public class UploadProductImagesCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
        public ICollection<IFormFile> Files { get; set; }
        public UploadProductImagesCommand(int productId, ICollection<IFormFile> files)
        {
            ProductId = productId;
            Files = files;
        }
    }

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

            if (product is null) throw new HttpRequestException("No product with this id");

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
                await _context.Images.AddAsync(image, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return true;
        }
    }
}
