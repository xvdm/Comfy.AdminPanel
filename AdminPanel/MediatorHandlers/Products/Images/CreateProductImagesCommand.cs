using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Models;
using AdminPanel.Services.Images.Upload;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Images
{
    public record CreateProductImagesCommand(int ProductId, ICollection<IFormFile> Files) : IRequest<bool>;


    public class CreateProductImagesCommandHandler : IRequestHandler<CreateProductImagesCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly IUploadImageToFileSystemService _uploadImageToFileSystemService;
        private readonly IPublisher _publisher;

        public CreateProductImagesCommandHandler(ApplicationDbContext context, IUploadImageToFileSystemService uploadImageToFileSystemService, IPublisher publisher)
        {
            _context = context;
            _uploadImageToFileSystemService = uploadImageToFileSystemService;
            _publisher = publisher;
        }

        public async Task<bool> Handle(CreateProductImagesCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

            if (product is null) return false;

            var newImage = false;

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

                newImage = true;
            }

            if (newImage)
            {
                var notification = new ProductInvalidatedEvent(request.ProductId, product.Url);
                await _publisher.Publish(notification, cancellationToken);
            }

            return true;
        }
    }
}