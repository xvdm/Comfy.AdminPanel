using AdminPanel.Data;
using MediatR;
using AdminPanel.Models;
using AdminPanel.Services;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Images
{
    public class UploadProductImageCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
        public IFormFile ImageFile { get; set; }
        public UploadProductImageCommand(int productId, IFormFile imageFile)
        {
            ProductId = productId;
            ImageFile = imageFile;
        }
    }

    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly IUploadImageToFileSystemService _uploadImageToFileSystemService;

        public UploadProductImageCommandHandler(ApplicationDbContext context, IUploadImageToFileSystemService uploadImageToFileSystemService)
        {
            _context = context;
            _uploadImageToFileSystemService = uploadImageToFileSystemService;
        }

        public async Task<bool> Handle(UploadProductImageCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

            if (product is null) throw new HttpRequestException("No product with this id");
            if (product.Images.Count >= 6) return false;

            var path = await _uploadImageToFileSystemService.UploadImage(request.ImageFile);

            var image = new Image
            {
                ProductId = request.ProductId,
                Url = path
            };
            await _context.Images.AddAsync(image, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
