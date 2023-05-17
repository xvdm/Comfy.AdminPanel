﻿using AdminPanel.Data;
using AdminPanel.Models.DTO;
using AdminPanel.Repositories;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Users;

public sealed record GetDTOUsersQuery(string? SearchString, bool GetLockoutUsers) : IRequest<IEnumerable<UserDTO>>;


public sealed class GetActiveDTOUsersQueryHandler : IRequestHandler<GetDTOUsersQuery, IEnumerable<UserDTO>>
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
        var usersDto = await _mapper
            .From(users.Where(x => request.GetLockoutUsers ? x.LockoutEnd != null : x.LockoutEnd == null))
            .ProjectToType<UserDTO>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var ids = usersDto.Select(x => x.Id);

        var claims = await _context.UserClaims
            .Where(x => ids.Contains(x.UserId))
            .Select(x => new { x.UserId, x.ClaimValue} )
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        foreach (var user in usersDto)
        {
            var position = claims.FirstOrDefault(x => x.UserId == user.Id);
            if (position is null) continue;
            user.Position = position.ClaimValue;
        }

        return usersDto;
    }
}