using AdminPanel.Data;
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

    public DeleteBannerCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _context.Banners.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (banner is null) return;
        _context.Banners.Remove(banner);
        await _context.SaveChangesAsync(cancellationToken);
    }
}