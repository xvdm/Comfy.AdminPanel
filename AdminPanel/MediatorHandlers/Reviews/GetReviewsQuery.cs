﻿using AdminPanel.Data;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Reviews;

public sealed record GetReviewsQuery : IRequest<IEnumerable<Review>>
{
    public bool IsActive { get; set; }

    private const int MaxPageSize = 15;
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

    public GetReviewsQuery(bool isActive, int? pageSize, int? pageNumber)
    {
        IsActive = isActive;
        if (pageSize is not null) PageSize = (int)pageSize;
        if (pageNumber is not null) PageNumber = (int)pageNumber;
    }
}

public sealed class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, IEnumerable<Review>>
{
    private readonly ApplicationDbContext _context;

    public GetReviewsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Review>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Reviews
            .Where(x => x.IsActive == request.IsActive)
            .Include(x => x.Answers.Where(y => y.IsActive == true))
                .ThenInclude(x => x.User)
            .Include(x => x.Product)
            .Include(x => x.User)
            .OrderByDescending(x => x.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}