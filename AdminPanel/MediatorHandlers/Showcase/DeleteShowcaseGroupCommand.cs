using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Showcase;

public sealed record DeleteShowcaseGroupCommand(int GroupId) : IRequest;


public sealed class DeleteShowcaseGroupCommandHandler : IRequestHandler<DeleteShowcaseGroupCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public DeleteShowcaseGroupCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(DeleteShowcaseGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.ShowcaseGroups
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.GroupId, cancellationToken);
        if (group is null) return;

        _context.ShowcaseGroups.Remove(group);
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new ShowcaseGroupsInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);
    }
}