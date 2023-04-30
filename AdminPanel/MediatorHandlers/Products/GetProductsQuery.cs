﻿using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products;

public record GetProductsQuery : IRequest<IEnumerable<Product>>
{
    public string? SearchString { get; set; }

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

    public GetProductsQuery(string? searchString, int? pageSize, int? pageNumber)
    {
        SearchString = searchString;
        if (pageSize is not null) PageSize = (int)pageSize;
        if (pageNumber is not null) PageNumber = (int)pageNumber;
    }
}


public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    private readonly ApplicationDbContext _context;

    public GetProductsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = _context.Products
            .AsNoTracking()
            .AsQueryable();

        if (request.SearchString is not null)
        {
            products = products.Where(x => x.Name.Contains(request.SearchString));
        }

        products = products
                .Include(x => x.Model)
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Characteristics);

        return await products
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
    }
}