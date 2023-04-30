using AdminPanel.Data;
using AdminPanel.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Banners;

public record EditBannerCommand(int Id, string Name, string PageUrl, IFormFile? Image) : IRequest;


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

        if (request.Image is not null)
        {
            _removeImageFromFileSystemService.RemoveImage(banner.ImageUrl);
            var path = await _uploadImageToFileSystemService.UploadImage(request.Image);
            banner.ImageUrl = path;
        }

        banner.Name = request.Name;
        banner.PageUrl = request.PageUrl;
        await _context.SaveChangesAsync(cancellationToken);
    }
}