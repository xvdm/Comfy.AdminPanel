using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Products.Images;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers;


[AutoValidateAntiforgeryToken]
[Authorize(Policy = RoleNames.Administrator)]
public sealed class ImagesController : Controller
{
    private readonly IMediator _mediator;

    public ImagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> UploadProductImage(int productId, ICollection<IFormFile> files)
    {
        await _mediator.Send(new CreateProductImagesCommand(productId, files));
        return LocalRedirect($"/Products/EditProduct/{productId}");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProductImage(int imageId, int productId)
    {
        await _mediator.Send(new DeleteProductImageCommand(imageId));
        return LocalRedirect($"/Products/EditProduct/{productId}");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMainCategoryImage(int categoryId, IFormFile file)
    {
        await _mediator.Send(new UpdateMainCategoryImageCommand(categoryId, file));
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateSubcategoryImage(int categoryId, IFormFile file)
    {
        await _mediator.Send(new UpdateSubcategoryImageCommand(categoryId, file));
        return Ok();
    }
}