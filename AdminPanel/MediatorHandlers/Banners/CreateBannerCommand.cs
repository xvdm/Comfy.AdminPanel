using AdminPanel.Data;
using AdminPanel.Models;
using AdminPanel.Services;
using MediatR;

namespace AdminPanel.MediatorHandlers.Banners;

public class CreateBannerCommand : IRequest
{
    public string Name { get; set; }
    public string PageUrl { get; set; }
    public IFormFile Image { get; set; }

    public CreateBannerCommand(string name, string pageUrl, IFormFile image)
    {
        Name = name;
        PageUrl = pageUrl;
        Image = image;
    }
}

public class CreateBannerCommandHandler : IRequestHandler<CreateBannerCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUploadImageToFileSystemService _uploadImageToFileSystemService;

    public CreateBannerCommandHandler(ApplicationDbContext context, IUploadImageToFileSystemService uploadImageToFileSystemService)
    {
        _context = context;
        _uploadImageToFileSystemService = uploadImageToFileSystemService;
    }

    public async Task Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        var path = await _uploadImageToFileSystemService.UploadImage(request.Image);

        var banner = new Banner()
        {
            Name = request.Name,
            ImageUrl = path,
            PageUrl = request.PageUrl
        };
        await _context.Banners.AddAsync(banner, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}