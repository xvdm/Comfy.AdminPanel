﻿using AdminPanel.Data;
using AdminPanel.Events.Invalidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Categories;

public record UpdateMainCategoryCommand(int Id, string Name) : IRequest;


public class UpdateMainCategoryCommandHandler : IRequestHandler<UpdateMainCategoryCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public UpdateMainCategoryCommandHandler(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(UpdateMainCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryWithNameCount = await _context.MainCategories.CountAsync(x => x.Name == request.Name, cancellationToken);
        if (categoryWithNameCount > 0) throw new HttpRequestException($"Main Category with name {request.Name} already exists");

        var category = await _context.MainCategories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (category is null) throw new HttpRequestException($"No MainCategory with id {request.Id}");

        category.Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);

        var notification = new CategoriesMenuInvalidatedEvent();
        await _publisher.Publish(notification, cancellationToken);
    }
}