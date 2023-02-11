using MediatR;

namespace AdminPanel.Commands.Users
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Role { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
    }
}
