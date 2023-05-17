using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Showcase;

public sealed record GetShowcaseGroupsQuery : IRequest<IEnumerable<ShowcaseGroup>>;


public sealed class GetShowcaseGroupsQueryHandler : IRequestHandler<GetShowcaseGroupsQuery, IEnumerable<ShowcaseGroup>>
{
    private readonly ApplicationDbContext _context;

    public GetShowcaseGroupsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ShowcaseGroup>> Handle(GetShowcaseGroupsQuery request, CancellationToken cancellationToken)
    {
        var groups = await _context.ShowcaseGroups
            .Include(x => x.Products)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return groups;
    }
}