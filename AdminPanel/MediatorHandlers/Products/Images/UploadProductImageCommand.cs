using AdminPanel.Data;
using MediatR;
using AdminPanel.Models;
using AdminPanel.Services;

namespace AdminPanel.MediatorHandlers.Products.Images
{
    public class UploadProductImageCommand : IRequest<string>
    {
        public int ProductId { get; set; }
        public IFormFile ImageFile { get; set; } = null!;
        public UploadProductImageCommand(int productId, IFormFile imageFile)
        {
            ProductId = productId;
            ImageFile = imageFile;
        }
    }

    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommand, string>
    {
        private readonly ApplicationDbContext _context;
        private readonly IUploadImageToFileSystemService _uploadImageToFileSystemService;

        public UploadProductImageCommandHandler(ApplicationDbContext context, IUploadImageToFileSystemService uploadImageToFileSystemService)
        {
            _context = context;
            _uploadImageToFileSystemService = uploadImageToFileSystemService;
        }

        public async Task<string> Handle(UploadProductImageCommand request, CancellationToken cancellationToken)
        {
            var path = await _uploadImageToFileSystemService.UploadImage(request.ImageFile);

            var image = new Image();
            image.ProductId = request.ProductId;
            image.Url = path;
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();

            return path;
        }
    }
}
