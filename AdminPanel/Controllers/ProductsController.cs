using AdminPanel.Handlers.Products;
using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Products.Brands;
using AdminPanel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;
using AdminPanel.Models.DTO;
using Mapster;
using AdminPanel.Models;
using AdminPanel.MediatorHandlers.Products.Categories;

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
            if (int.TryParse(productId, out int productIdInt) == false)
            {
                return BadRequest("ChangeProductActivityStatus :: Parsing error :: productId");
            }
            if (bool.TryParse(isActive, out bool isActiveBool) == false)
            {
                return BadRequest("ChangeProductActivityStatus :: Parsing error :: isActive");
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
            if (int.TryParse(productId, out int productIdInt) == false)
            {
                return BadRequest("EditCharacteristic :: Parsing error :: productId");
            }
            if (int.TryParse(id, out int idInt) == false)
            {
                return BadRequest("EditCharacteristic :: Parsing error :: id");
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
                return BadRequest("DeleteCharacteristic :: Parsing error :: productId");
            }
            if (int.TryParse(id, out var idInt) == false)
            {
                return BadRequest("DeleteCharacteristic :: Parsing error :: id");
            }
            await _mediator.Send(new DeleteProductCharacteristicCommand(idInt));
            return LocalRedirect($"/Products/EditProduct/{productId}");
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacteristic(string productId, string name, string value)
        {
            if (int.TryParse(productId, out int productIdInt) == false)
            {
                return BadRequest("AddCharacteristic :: Parsing error :: productId");
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
