﻿using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Categories;

public record CreateSubcategoryCommand(Subcategory Category) : IRequest;


public class CreateSubcategoryCommandHandler : IRequestHandler<CreateSubcategoryCommand>
{
    private readonly ApplicationDbContext _context;

    public CreateSubcategoryCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateSubcategoryCommand request, CancellationToken cancellationToken)
    {
        var mainCategory = await _context.MainCategories.FirstOrDefaultAsync(x => x.Id == request.Category.MainCategoryId, cancellationToken);
        if (mainCategory is null)
        {
            return;
        }
        await _context.Subcategories.AddAsync(request.Category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}