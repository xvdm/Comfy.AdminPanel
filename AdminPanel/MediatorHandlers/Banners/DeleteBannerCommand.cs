using AdminPanel.Data;
using AdminPanel.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Banners;

public class DeleteBannerCommand : IRequest
{
    public int Id { get; set; }

    public DeleteBannerCommand(int id)
    {
        Id = id;
    }
}

public class DeleteBannerCommandHandler : IRequestHandler<DeleteBannerCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IRemoveImageFromFileSystemService _removeImageFromFileSystemService;

    public DeleteBannerCommandHandler(ApplicationDbContext context, IRemoveImageFromFileSystemService removeImageFromFileSystemService)
    {
        _context = context;
        _removeImageFromFileSystemService = removeImageFromFileSystemService;
    }

    public async Task Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _context.Banners.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (banner is null) return;

        _removeImageFromFileSystemService.RemoveImage(banner.ImageUrl);

        _context.Banners.Remove(banner);
        await _context.SaveChangesAsync(cancellationToken);
    }
}