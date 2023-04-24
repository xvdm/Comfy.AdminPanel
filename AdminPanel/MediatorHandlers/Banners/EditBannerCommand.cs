using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Banners;

public class EditBannerCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string PageUrl { get; set; }

    public EditBannerCommand(int id, string name, string imageUrl, string pageUrl)
    {
        Id = id;
        Name = name;
        ImageUrl = imageUrl;
        PageUrl = pageUrl;
    }
}

public class EditBannerCommandHandler : IRequestHandler<EditBannerCommand>
{
    private readonly ApplicationDbContext _context;

    public EditBannerCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _context.Banners.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (banner is null) return;

        banner.Name = request.Name;
        banner.ImageUrl = request.ImageUrl;
        banner.PageUrl = request.PageUrl;
        await _context.SaveChangesAsync(cancellationToken);
    }
}