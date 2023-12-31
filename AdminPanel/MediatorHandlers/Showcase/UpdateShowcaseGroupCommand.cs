﻿using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Showcase;

public sealed record UpdateShowcaseGroupCommand(int GroupId, string Name, int SubcategoryId, string? QueryString) : IRequest;


public sealed class UpdateShowcaseGroupCommandHandler : IRequestHandler<UpdateShowcaseGroupCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public UpdateShowcaseGroupCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(UpdateShowcaseGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.ShowcaseGroups.FirstOrDefaultAsync(x => x.Id == request.GroupId, cancellationToken);
        if (group is null) return;

        var groupsWithNameCount = await _context.ShowcaseGroups.CountAsync(x => x.Name == request.Name, cancellationToken);
        if (groupsWithNameCount > 0) return;

        group.Name = request.Name;
        group.QueryString = request.QueryString;
        group.SubcategoryId = request.SubcategoryId;
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new ShowcaseGroupsInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);
    }
}