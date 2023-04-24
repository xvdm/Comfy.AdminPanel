using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;

namespace AdminPanel.MediatorHandlers.Banners;

public class CreateBannerCommand : IRequest
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string PageUrl { get; set; }

    public CreateBannerCommand(string name, string imageUrl, string pageUrl)
    {
        Name = name;
        ImageUrl = imageUrl;
        PageUrl = pageUrl;
    }
}

public class CreateBannerCommandHandler : IRequestHandler<CreateBannerCommand>
{
    private readonly ApplicationDbContext _context;

    public CreateBannerCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = new Banner()
        {
            Name = request.Name,
            ImageUrl = request.ImageUrl,
            PageUrl = request.PageUrl
        };
        await _context.Banners.AddAsync(banner, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}