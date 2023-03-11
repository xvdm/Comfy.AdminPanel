using AdminPanel.Data;
using MediatR;
using AdminPanel.Models;

namespace AdminPanel.MediatorHandlers.Product.Images
{
    public class UploadProductImageCommand : IRequest
    {
        public int ProductId { get; set; }
        public IFormFile ImageFile { get; set; } = null!;
        public UploadProductImageCommand(int productId, IFormFile imageFile)
        {
            ProductId = productId;
            ImageFile = imageFile;
        }
    }

    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommand>
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public UploadProductImageCommandHandler(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task Handle(UploadProductImageCommand request, CancellationToken cancellationToken)
        {
            string filetype = request.ImageFile.FileName.Substring(request.ImageFile.FileName.LastIndexOf('.') + 1);
            Guid guid = Guid.NewGuid();
            string path = $"/images/{guid}.{filetype}";
            using (var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
            {
                await request.ImageFile.CopyToAsync(fileStream);
            }

            var image = new Image();
            image.ProductId = request.ProductId;
            image.Url = path;
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
        }
    }
}
