using AdminPanel.Data;
using AdminPanel.Services.Images.Remove;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Products.Images;

public sealed record RemoveMainCategoryImageCommand(int Id) : IRequest;


public sealed class RemoveMainCategoryImageCommandHandler : IRequestHandler<RemoveMainCategoryImageCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IRemoveImageFromFileSystemService _removeImageFromFileSystemService;

    public RemoveMainCategoryImageCommandHandler(ApplicationDbContext context, IRemoveImageFromFileSystemService removeImageFromFileSystemService)
    {
        _context = context;
        _removeImageFromFileSystemService = removeImageFromFileSystemService;
    }

    public async Task Handle(RemoveMainCategoryImageCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.MainCategories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (category is null) throw new HttpRequestException($"No category with id {request.Id}");

        if (string.IsNullOrEmpty(category.ImageUrl) == false)
        {
            await _removeImageFromFileSystemService.RemoveAsync(category.ImageUrl);
        }

        category.ImageUrl = "";
        await _context.SaveChangesAsync(cancellationToken);
    }
}