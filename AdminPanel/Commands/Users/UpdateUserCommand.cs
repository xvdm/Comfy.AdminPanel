using MediatR;

namespace AdminPanel.Commands.Users
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public string Position { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
    }
}
