using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Showcase
{
    public class EditShowcaseGroupCommand : IRequest
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string QueryString { get; set; }

        public EditShowcaseGroupCommand(int groupId, string name, string queryString)
        {
            GroupId = groupId;
            Name = name;
            QueryString = queryString;
        }
    }

    public class EditShowcaseGroupCommandHandler : IRequestHandler<EditShowcaseGroupCommand>
    {
        private readonly ApplicationDbContext _context;

        public EditShowcaseGroupCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(EditShowcaseGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _context.ShowcaseGroups.FirstOrDefaultAsync(x => x.Id == request.GroupId, cancellationToken);
            if (group is null) return;

            var groupWithName = await _context.ShowcaseGroups.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
            if (groupWithName is not null) return;

            group.Name = request.Name;
            group.QueryString = request.QueryString;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
