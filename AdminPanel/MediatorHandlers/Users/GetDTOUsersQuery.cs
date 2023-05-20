using AdminPanel.Data;
using AdminPanel.Models.DTO;
using AdminPanel.Models.Identity;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Users;

public sealed record GetDTOUsersQuery(string? SearchString, bool GetLockoutUsers) : IRequest<IEnumerable<UserDTO>>;


public sealed class GetActiveDTOUsersQueryHandler : IRequestHandler<GetDTOUsersQuery, IEnumerable<UserDTO>>
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetActiveDTOUsersQueryHandler(IMapper mapper, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _context = context;
        _userManager = userManager;
    }

    public async Task<IEnumerable<UserDTO>> Handle(GetDTOUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _context.Users
            .AsNoTracking()
            .AsQueryable()
            .Where(x => request.GetLockoutUsers ? x.LockoutEnd != null : x.LockoutEnd == null);
        
        if (request.SearchString is not null)
        {
            users = users.Where(x =>
                x.UserName.Contains(request.SearchString) ||
                x.Email.Contains(request.SearchString) ||
                x.PhoneNumber.Contains(request.SearchString)
            );
        }

        var usersDto = await _mapper
            .From(users)
            .ProjectToType<UserDTO>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var ids = usersDto.Select(x => x.Id).ToList();

        for (var i = 0; i < ids.Count; i++)
        {
            var roles = await _userManager.GetRolesAsync(usersDto[i].Adapt<ApplicationUser>());
            usersDto[i].Position = roles.FirstOrDefault() ?? "";
        }

        return usersDto;
    }
}