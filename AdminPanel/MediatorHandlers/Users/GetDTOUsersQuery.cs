using AdminPanel.Data;
using AdminPanel.Models.DTO;
using AdminPanel.Models.Identity;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Users;

public sealed record GetDTOUsersQuery : IRequest<IEnumerable<UserDTO>>
{
    public string? SearchString { get; set; }
    public bool GetLockoutUsers { get; set; }

    private const int MaxPageSize = 10;
    private int _pageSize = MaxPageSize;
    private int _pageNumber = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value is > MaxPageSize or < 1 ? MaxPageSize : value;
    }
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = (value < 1) ? 1 : value;
    }

    public GetDTOUsersQuery(string? searchString, bool getLockoutUsers, int? pageSize, int? pageNumber)
    {
        SearchString = searchString;
        GetLockoutUsers = getLockoutUsers;
        if (pageSize is not null) PageSize = (int)pageSize;
        if (pageNumber is not null) PageNumber = (int)pageNumber;
    }
}


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
                x.Name.Contains(request.SearchString) ||
                x.Email.Contains(request.SearchString) ||
                x.PhoneNumber.Contains(request.SearchString)
            );
        }

        users = users
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize);

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