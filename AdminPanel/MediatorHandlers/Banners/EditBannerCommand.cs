using AdminPanel.Data;
using AdminPanel.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Banners;

public class EditBannerCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PageUrl { get; set; }
    public IFormFile Image { get; set; }

    public EditBannerCommand(int id, string name, string pageUrl, IFormFile image)
    {
        Id = id;
        Name = name;
        PageUrl = pageUrl;
        Image = image;
    }
}

public class EditBannerCommandHandler : IRequestHandler<EditBannerCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUploadImageToFileSystemService _uploadImageToFileSystemService;
    private readonly IRemoveImageFromFileSystemService _removeImageFromFileSystemService;

    public EditBannerCommandHandler(ApplicationDbContext context, IUploadImageToFileSystemService uploadImageToFileSystemService, IRemoveImageFromFileSystemService removeImageFromFileSystemService)
    {
        _context = context;
        _uploadImageToFileSystemService = uploadImageToFileSystemService;
        _removeImageFromFileSystemService = removeImageFromFileSystemService;
    }

    public async Task Handle(EditBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _context.Banners.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (banner is null) return;

        _removeImageFromFileSystemService.RemoveImage(banner.ImageUrl);
        var path = await _uploadImageToFileSystemService.UploadImage(request.Image);

        banner.Name = request.Name;
        banner.ImageUrl = path;
        banner.PageUrl = request.PageUrl;
        await _context.SaveChangesAsync(cancellationToken);
    }
}