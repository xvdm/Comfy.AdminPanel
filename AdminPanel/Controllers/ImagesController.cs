using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Product.Images;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> UploadImage(int productId, IFormFile file)
        {
            if (file is not null)
            {
                await _mediator.Send(new UploadProductImageCommand(productId, file));

                return LocalRedirect($"/Products/EditProduct/{productId}");
            }

            return BadRequest("No file was uploaded.");
        }

        public async Task<IActionResult> DeleteImage(int imageId, int productId)
        {
            await _mediator.Send(new DeleteProductImageCommand(imageId));

            return LocalRedirect($"/Products/EditProduct/{productId}");
        }
    }
}
