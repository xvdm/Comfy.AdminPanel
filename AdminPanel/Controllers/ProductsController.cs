using AdminPanel.Handlers.Products;
using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Products.Brands;
using AdminPanel.MediatorHandlers.Products.Categories;
using AdminPanel.MediatorHandlers.Products.Models;
using AdminPanel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;
using AdminPanel.Models.DTO;
using Mapster;

namespace AdminPanel.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        public async Task<IActionResult> Products(int categoryId, string filterQuery)
        {
            //товары в подкатегории

            if (ProductUrl.TryRemoveEmptyAndDuplicatesFromQuery(filterQuery, out Dictionary<string, List<string>> queryDictionary))
            {
                return LocalRedirect($"/Products/Products?categoryId={categoryId}&{WebUtility.UrlEncode(ProductUrl.GetUrlQuery(queryDictionary))}");
            }

            var products = await _mediator.Send(new GetProductsQuery(categoryId, 0, 10, queryDictionary));
            var brands = await _mediator.Send(new GetBrandsQuery());
            var models = await _mediator.Send(new GetModelsQuery());

            var viewModel = new ProductsViewModel()
            {
                CategoryId = categoryId,
                Query = filterQuery,
                Products = products,
                Brands = brands,
                Models = models
            };

            return View(viewModel);
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));

            if (product is null)
            {
                return NotFound(product);
            }
            return View(product);
        }

        public async Task<IActionResult> ChangeProductActivityStatus(int productId, bool isActive)
        {
            await _mediator.Send(new ChangeProductActivityStatusCommand(productId, isActive));

            return LocalRedirect($"/Products/EditProduct/{productId}");
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return View(productDto);
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
    }
}
