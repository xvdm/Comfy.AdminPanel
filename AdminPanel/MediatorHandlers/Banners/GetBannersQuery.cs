using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Banners;

public record GetBannersQuery : IRequest<ICollection<Banner>>;


public class GetBannersQueryHandler : IRequestHandler<GetBannersQuery, ICollection<Banner>>
{
    private readonly ApplicationDbContext _context;

    public GetBannersQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Banner>> Handle(GetBannersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Banners
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}