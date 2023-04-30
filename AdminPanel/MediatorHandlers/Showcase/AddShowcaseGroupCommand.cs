using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Showcase;

public record AddShowcaseGroupCommand(string Name, string QueryString) : IRequest;


public class AddShowcaseGroupCommandHandler : IRequestHandler<AddShowcaseGroupCommand>
{
    private readonly ApplicationDbContext _context;

    public AddShowcaseGroupCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddShowcaseGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.ShowcaseGroups.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
        if (group is not null) return;

        var newGroup = new ShowcaseGroup()
        {
            Name = request.Name,
            QueryString = request.QueryString
        };
        await _context.ShowcaseGroups.AddAsync(newGroup, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}