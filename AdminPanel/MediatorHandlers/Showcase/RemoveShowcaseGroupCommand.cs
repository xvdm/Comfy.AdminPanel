using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Showcase;

public record RemoveShowcaseGroupCommand(int GroupId) : IRequest;


public class RemoveShowcaseGroupCommandHandler : IRequestHandler<RemoveShowcaseGroupCommand>
{
    private readonly ApplicationDbContext _context;

    public RemoveShowcaseGroupCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveShowcaseGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.ShowcaseGroups.FirstOrDefaultAsync(x => x.Id == request.GroupId, cancellationToken);
        if (group is null) return;

        _context.ShowcaseGroups.Remove(group);
        await _context.SaveChangesAsync(cancellationToken);
    }
}