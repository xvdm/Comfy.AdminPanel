﻿using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Showcase;

public record CreateShowcaseGroupCommand(string Name, string QueryString) : IRequest;


public class CreateShowcaseGroupCommandHandler : IRequestHandler<CreateShowcaseGroupCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public CreateShowcaseGroupCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(CreateShowcaseGroupCommand request, CancellationToken cancellationToken)
    {
        var groupWithName = await _context.ShowcaseGroups.CountAsync(x => x.Name == request.Name, cancellationToken);
        if (groupWithName > 0) return;

        var newGroup = new ShowcaseGroup
        {
            Name = request.Name,
            QueryString = request.QueryString
        };
        _context.ShowcaseGroups.Add(newGroup);
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new ShowcaseGroupsInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);
    }
}