using AdminPanel.Handlers.Products;
using AdminPanel.MediatorHandlers.Products.Categories;
using AdminPanel.Models.DTO;
using AdminPanel.Models.ViewModels;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> CreateProduct()
        {
            var viewModel = await GetCategoriesViewModel();
            return View(viewModel);
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            var viewModel = await GetProductCategoriesViewModel(id);
            if(viewModel.Product is null)
            {
                return NotFound(viewModel.Product);
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Products(string? searchString, int? pageSize, int? pageNumber)
        {
            var products = await _mediator.Send(new GetProductsQuery(searchString, pageSize, pageNumber));
            var viewModel = new ProductsViewModel()
            {
                Products = products
            };
            return View(viewModel);
        }


        public async Task<IActionResult> ChangeProductActivityStatus(string productId, string isActive)
        {
            if (int.TryParse(productId, out var productIdInt) == false)
            {
                return BadRequest("ChangeProductActivityStatus :: productId :: parse to int error");
            }
            if (bool.TryParse(isActive, out var isActiveBool) == false)
            {
                return BadRequest("ChangeProductActivityStatus :: isActive :: parse to bool error");
            }
            await _mediator.Send(new ChangeProductActivityStatusCommand(productIdInt, isActiveBool));
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = await GetCategoriesViewModel();
                return View(viewModel);
            }
            var command = productDto.Adapt<CreateProductCommand>();
            var productId = await _mediator.Send(command);
            return LocalRedirect($"/Products/EditProduct/{productId}");
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Incorrect data was passed");
            }
            var command = productDto.Adapt<EditProductCommand>();
            var productId = await _mediator.Send(command);
            return LocalRedirect($"/Products/EditProduct/{productId}");
        }

        [HttpPost]
        public async Task<IActionResult> EditCharacteristic(string productId, string id, string name, string value)
        {
            Console.WriteLine($"productId = {productId}");
            if (int.TryParse(productId, out var productIdInt) == false)
            {
                return BadRequest("EditCharacteristic :: productId :: parse to int error");
            }
            if (int.TryParse(id, out var idInt) == false)
            {
                return BadRequest("EditCharacteristic :: id :: parse to int error");
            }

            var product = await _mediator.Send(new GetProductByIdQuery(productIdInt));
            if (product is null)
            {
                return NotFound("No product with given Id was found");
            }
            var editCharacteristicCommand = new EditProductCharacteristicCommand(product, idInt, name, value);
            await _mediator.Send(editCharacteristicCommand);
            return LocalRedirect($"/Products/EditProduct/{productId}");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCharacteristic(string productId, string id)
        {
            if (int.TryParse(productId, out var _) == false)
            {
                return BadRequest("DeleteCharacteristic :: productId :: parse to int error");
            }
            if (int.TryParse(id, out var idInt) == false)
            {
                return BadRequest("DeleteCharacteristic :: id :: parse to int error");
            }
            await _mediator.Send(new DeleteProductCharacteristicCommand(idInt));
            return LocalRedirect($"/Products/EditProduct/{productId}");
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacteristic(string productId, string name, string value)
        {
            if (int.TryParse(productId, out var productIdInt) == false)
            {
                return BadRequest("AddCharacteristic :: productId :: parse to int error");
            }
            var addCharacteristic = new AddProductCharacteristicCommand(productIdInt, name, value);
            await _mediator.Send(addCharacteristic);
            return LocalRedirect($"/Products/EditProduct/{productId}");
        }

        private async Task<ProductCategoriesViewModel> GetProductCategoriesViewModel(int productId)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(productId));
            if(product is null)
            {
                throw new HttpRequestException($"No product with id {productId} was found");
            }
            var mainCategories = await _mediator.Send(new GetMainCategoriesQuery());
            var subcategories = await _mediator.Send(new GetSubcategoriesForMainCategoryQuery(product.Category.MainCategoryId));
            var viewModel = new ProductCategoriesViewModel()
            {
                Product = product,
                MainCategories = mainCategories,
                Subcategories = subcategories
            };
            return viewModel;
        }

        private async Task<CategoriesViewModel> GetCategoriesViewModel()
        {
            var mainCategories = await _mediator.Send(new GetMainCategoriesQuery());
            var subcategories = await _mediator.Send(new GetAllSubcategoriesQuery());

            var viewModel = new CategoriesViewModel()
            {
                MainCategories = mainCategories,
                Subcategories = subcategories
            };
            return viewModel;
        }
    }
}
