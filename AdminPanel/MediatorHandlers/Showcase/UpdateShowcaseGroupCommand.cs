using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Showcase;

public record UpdateShowcaseGroupCommand(int GroupId, string Name, string QueryString) : IRequest;


public class UpdateShowcaseGroupCommandHandler : IRequestHandler<UpdateShowcaseGroupCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateShowcaseGroupCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateShowcaseGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.ShowcaseGroups.FirstOrDefaultAsync(x => x.Id == request.GroupId, cancellationToken);
        if (group is null) return;

        var groupWithName = await _context.ShowcaseGroups.CountAsync(x => x.Name == request.Name, cancellationToken);
        if (groupWithName > 0) return;

        group.Name = request.Name;
        group.QueryString = request.QueryString;
        await _context.SaveChangesAsync(cancellationToken);
    }
}