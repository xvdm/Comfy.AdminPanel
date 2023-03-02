using AdminPanel.Handlers.Products;
using AdminPanel.Handlers.Products.Brands;
using AdminPanel.Handlers.Products.Categories;
using AdminPanel.Handlers.Products.Models;
using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Products.Brands;
using AdminPanel.MediatorHandlers.Products.Categories;
using AdminPanel.MediatorHandlers.Products.Models;
using AdminPanel.Models.DTO;
using AdminPanel.Models.ViewModels;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;
using WebApplication2.Models;

namespace AdminPanel.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Policy = PoliciesNames.Administrator)]
    public class AdminPanelController : Controller
    {
        private readonly IMediator _mediator;

        public AdminPanelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        public IActionResult CreateBrand()
        {
            return View();
        }

        public IActionResult CreateModel()
        {
            return View();
        }
        public IActionResult CreateCategory()
        {
            return View();
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

        public async Task<IActionResult> GetProducts(string? query)
        {
            if(ProductUrl.TryRemoveEmptyAndDuplicatesFromQuery(query, out Dictionary<string, List<string>> queryDictionary))
            {
                return LocalRedirect($"/AdminPanel/{WebUtility.UrlEncode(ProductUrl.GetUrlQuery(queryDictionary))}");
            }
            

            var products = await _mediator.Send(new GetProductsQuery(0, 10, queryDictionary));
            var categories = await _mediator.Send(new GetCategoriesQuery());
            var brands = await _mediator.Send(new GetBrandsQuery());
            var models = await _mediator.Send(new GetModelsQuery());

            var viewModel = new ProductListViewModel()
            {
                Query = query,
                Products = products,
                Brands = brands,
                Models = models,
                Categories = categories
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ChangeProductActivityStatus(int productId, bool isActive)
        {
            await _mediator.Send(new ChangeProductActivityStatusCommand(productId, isActive));

            return LocalRedirect($"/AdminPanel/EditProduct/{productId}");
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO productDto)
        {
            if(!ModelState.IsValid)
            {
                return View(productDto);
            }

            var command = productDto.Adapt<CreateProductCommand>();
            var productId = await _mediator.Send(command);

            return LocalRedirect($"/AdminPanel/EditProduct/{productId}");
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
        public async Task<IActionResult> EditCharacteristic(int productId, int id, string name, string value)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(productId));
            if (product is null) return NotFound("No product with given Id was found");

            var editCharacteristicCommand = new EditProductCharacteristicCommand()
            {
                Product = product,
                CharacteristicId = id,
                Name = name,
                Value = value
            };
            await _mediator.Send(editCharacteristicCommand);

            return LocalRedirect($"/AdminPanel/EditProduct/{productId}");
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteCharacteristic(int productId, int id)
        {
            await _mediator.Send(new DeleteProductCharacteristicCommand(id));
            return LocalRedirect($"/AdminPanel/EditProduct/{productId}");
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacteristic(int productId, string name, string value)
        {
            var command = new AddProductCharacteristicCommand()
            {
                ProductId = productId,
                Name = name,
                Value = value
            };
            await _mediator.Send(command);

            return LocalRedirect($"/AdminPanel/EditProduct/{productId}");
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(Brand brand)
        {
            await _mediator.Send(new CreateBrandCommand(brand));
            return View(brand);
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel(Model model)
        {
            await _mediator.Send(new CreateModelCommand(model));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            await _mediator.Send(new CreateCategoryCommand(category));
            return View(category);
        }

        public IActionResult GetAutocompleteBrands(string input)
        {
            var items = _mediator.Send(new GetAutocompleteBrandsQuery(input, 5));
            return Content(string.Join("", items.Result.Select(item => $"<option class='autocomplete-item'>{item.Name}</option>")));
        }

        public IActionResult GetAutocompleteCategories(string input)
        {
            var items = _mediator.Send(new GetAutocompleteCategoriesQuery(input, 5));
            return Content(string.Join("", items.Result.Select(item => $"<option class='autocomplete-item'>{item.Name}</option>")));
        }

        public IActionResult GetAutocompleteModels(string input)
        {
            var items = _mediator.Send(new GetAutocompleteModelsQuery(input, 5));
            return Content(string.Join("", items.Result.Select(item => $"<option class='autocomplete-item'>{item.Name}</option>")));
        } 
    }
}
