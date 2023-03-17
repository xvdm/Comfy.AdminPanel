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
        public async Task<IActionResult> UploadProductImage(string productId, IList<IFormFile> files)
        {
            if(int.TryParse(productId, out int productIdInt) == false)
            {
                return BadRequest("UploadProductImage :: parseError :: productId");
            }

            var paths = new List<string>();

            foreach(var file in files) {
                if (file is null)
                {
                    return BadRequest("No file was uploaded.");
                }
                var filePath = await _mediator.Send(new UploadProductImageCommand(productIdInt, file));
                paths.Add(filePath);
            }
            
            return Ok(paths);
        }

        public async Task<IActionResult> DeleteProductImage(int imageId, int productId)
        {
            await _mediator.Send(new DeleteProductImageCommand(imageId));
            return LocalRedirect($"/Products/EditProduct/{productId}");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMainCategoryImage(int categoryId, IFormFile file)
        {
            if (file is null)
            {
                return BadRequest("No file was uploaded.");
            }
            await _mediator.Send(new UpdateMainCategoryImageCommand(categoryId, file));
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubcategoryImage(int categoryId, IFormFile file)
        {
            if (file is null)
            {
                return BadRequest("No file was uploaded.");
            }
            await _mediator.Send(new UpdateSubcategoryImageCommand(categoryId, file));
            return Ok();
        }
    }
}
