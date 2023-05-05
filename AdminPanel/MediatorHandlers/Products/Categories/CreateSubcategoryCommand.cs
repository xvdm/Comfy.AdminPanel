﻿using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using AdminPanel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Categories;

public record CreateSubcategoryCommand(Subcategory Category) : IRequest;


public class CreateSubcategoryCommandHandler : IRequestHandler<CreateSubcategoryCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public CreateSubcategoryCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(CreateSubcategoryCommand request, CancellationToken cancellationToken)
    {
        var mainCategoryCount = await _context.MainCategories.CountAsync(x => x.Id == request.Category.MainCategoryId, cancellationToken);
        if (mainCategoryCount <= 0) return;

        _context.Subcategories.Add(request.Category);
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new CategoriesMenuInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);
    }
}