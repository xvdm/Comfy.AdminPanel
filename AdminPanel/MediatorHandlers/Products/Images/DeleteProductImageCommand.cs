using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace AdminPanel.MediatorHandlers.Product.Images
{
    public class DeleteProductImageCommand : IRequest
    {
        public int ImageId { get; set; }
        public DeleteProductImageCommand(int imageId)
        {
            ImageId = imageId;
        }
    }


    public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommand>
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DeleteProductImageCommandHandler(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
        {
            var image = await _context.Images.FirstAsync(x => x.Id == request.ImageId);
            if (image is null)
            {
                throw new HttpRequestException("Image was not found");
            }
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();


            //  Path.Combine возвращает значение второго параметра, если в нем указан абсолютный путь (начинающийся на /)
            string fullPath = Path.Combine(_env.WebRootPath, image.Url);
            if (fullPath == image.Url) fullPath = _env.WebRootPath + image.Url;

            if (File.Exists(fullPath))
            {
                try
                {
                    File.Delete(fullPath);
                }
                catch (Exception e)
                {
                    //Debug.WriteLine(e.Message);
                }
            }
        }
    }
}
