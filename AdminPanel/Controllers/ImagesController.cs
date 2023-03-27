using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Products.Images;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Policy = PoliciesNames.Administrator)]
    public class ImagesController : Controller
    {
        private readonly IMediator _mediator;

        public ImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> UploadProductImage(int productId, IFormFile file)
        {
            await _mediator.Send(new UploadProductImageCommand(productId, file));
            return LocalRedirect($"/Products/EditProduct/{productId}");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductImage(int imageId, int productId)
        {
            //if (int.TryParse(imageId, out var imageIdInt) == false)
            //{
            //    return BadRequest("DeleteProductImage :: imageId :: parse to int error");
            //}
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
}
