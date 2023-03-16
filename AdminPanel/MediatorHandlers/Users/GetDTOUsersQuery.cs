using AdminPanel.Repositories;
using AdminPanel.Models.DTO;
using AdminPanel.Data;
using MapsterMapper;
using MediatR;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Handlers.Users
{
    public class GetDTOUsersQuery : IRequest<IEnumerable<UserDTO>>
    {
        public string? SearchString { get; set; }
        public bool GetLockoutUsers { get; }
        public GetDTOUsersQuery(string? searchString, bool getLockoutUsers)
        {
            SearchString = searchString;
            GetLockoutUsers = getLockoutUsers;
        }
    }


    public class GetActiveDTOUsersQueryHandler : IRequestHandler<GetDTOUsersQuery, IEnumerable<UserDTO>>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public GetActiveDTOUsersQueryHandler(IUsersRepository usersRepository, IMapper mapper, ApplicationDbContext context)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<UserDTO>> Handle(GetDTOUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _usersRepository.GetUsers(request.SearchString);
            var usersDTO = await _mapper
                .From(users.Where(x => request.GetLockoutUsers ? x.LockoutEnd != null : x.LockoutEnd == null))
                .ProjectToType<UserDTO>()
                .ToListAsync(cancellationToken);

            foreach (var user in usersDTO)
            {
                var claim = _context.UserClaims.FirstOrDefault(x => x.UserId == user.Id);
                if (claim is not null)
                {
                    user.Position = claim.ClaimValue;
                }
            }
            return usersDTO;
        }
    }
}
