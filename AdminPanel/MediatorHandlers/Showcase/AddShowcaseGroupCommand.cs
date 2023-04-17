using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Showcase
{
    public class AddShowcaseGroupCommand : IRequest
    {
        public string Name { get; set; }
        public string QueryString { get; set; }

        public AddShowcaseGroupCommand(string name, string queryString)
        {
            Name = name;
            QueryString = queryString;
        }
    }

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
}
