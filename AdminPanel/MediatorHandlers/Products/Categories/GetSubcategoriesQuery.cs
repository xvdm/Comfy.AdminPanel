﻿using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AdminPanel.Models;

namespace AdminPanel.MediatorHandlers.Products.Categories
{
    public class GetSubcategoriesQuery : IRequest<IEnumerable<Subcategory>>
    {
        public int MainCategoryId { get; set; }
        public GetSubcategoriesQuery(int mainCategoryId)
        {
            MainCategoryId = mainCategoryId;
        }
    }

    public class GetSubcategoriesQueryHandler : IRequestHandler<GetSubcategoriesQuery, IEnumerable<Subcategory>>
    {
        private readonly ApplicationDbContext _context;

        public GetSubcategoriesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subcategory>> Handle(GetSubcategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Subcategories
                .Where(x => x.MainCategoryId == request.MainCategoryId)
                .ToListAsync(cancellationToken);
        }
    }
}
