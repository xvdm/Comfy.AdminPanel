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

        public async Task<IActionResult> Products(int? pageSize, int? pageNumber, int? categoryId, string? filterQuery)
        {
            if (ProductUrl.TryRemoveEmptyAndDuplicatesFromQuery(filterQuery, out Dictionary<string, List<string>> queryDictionary))
            {
                string redirectUrl = $"/Products/Products?";
                if (pageSize is not null) redirectUrl += $"pageSize={pageSize}&";
                if (pageNumber is not null) redirectUrl += $"pageNumber={pageNumber}&";
                if (categoryId is not null) redirectUrl += $"categoryId={categoryId}&";
                if (filterQuery is not null) redirectUrl += $"filterQuery={WebUtility.UrlEncode(ProductUrl.GetUrlQuery(queryDictionary))}";

                return LocalRedirect(redirectUrl);
            }

            Subcategory? category = null;
            IEnumerable<Brand>? brands = null;
            Dictionary<CharacteristicName, List<CharacteristicValue>>? characteristicsDictionary = null;
            if (categoryId is not null)
            {
                category = await _mediator.Send(new GetSubcategoryByIdQuery(categoryId));
                brands = await _mediator.Send(new GetBrandsQuery(categoryId));
                if (category is null)
                {
                    return NotFound(category);
                }
                characteristicsDictionary = GetCharacteristicsInCategory(category);
            }
            var products = await _mediator.Send(new GetProductsQuery(pageSize, pageNumber, categoryId, queryDictionary));

            var viewModel = new ProductsViewModel()
            {
                CategoryId = categoryId,
                Characteristics = characteristicsDictionary,
                Query = filterQuery,
                Products = products,
                Brands = brands
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

            var mainCategories = await _mediator.Send(new GetMainCategoriesQuery());
            var subcategories = await _mediator.Send(new GetAllSubcategoriesQuery());

            var viewModel = new EditProductViewModel()
            {
                Product = product,
                MainCategories = mainCategories,
                Subcategories = subcategories
            };
            return View(viewModel);
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

        [HttpPost]
        public async Task<IActionResult> EditCharacteristic(int productId, int id, string name, string value)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(productId));
            if (product is null)
            {
                return NotFound("No product with given Id was found");
            }
            var editCharacteristicCommand = new EditProductCharacteristicCommand(product, id, name, value);
            await _mediator.Send(editCharacteristicCommand);
            return LocalRedirect($"/Products/EditProduct/{productId}");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCharacteristic(int productId, int id)
        {
            await _mediator.Send(new DeleteProductCharacteristicCommand(id));
            return LocalRedirect($"/Products/EditProduct/{productId}");
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacteristic(int productId, string name, string value)
        {
            var addCharacteristic = new AddProductCharacteristicCommand(productId, name, value);
            await _mediator.Send(addCharacteristic);
            return LocalRedirect($"/Products/EditProduct/{productId}");
        }

        private Dictionary<CharacteristicName, List<CharacteristicValue>>? GetCharacteristicsInCategory(Subcategory? category)
        {
            if (category is null) return null;

            var characteristicsDictionary = new Dictionary<CharacteristicName, List<CharacteristicValue>>();
            foreach (var characteristic in category.UniqueCharacteristics)
            {
                if (characteristicsDictionary.TryGetValue(characteristic.CharacteristicsName, out List<CharacteristicValue>? characteristicValues))
                {
                    characteristicValues.Add(characteristic.CharacteristicsValue);
                }
                else
                {
                    var list = new List<CharacteristicValue>();
                    list.Add(characteristic.CharacteristicsValue);
                    characteristicsDictionary.Add(characteristic.CharacteristicsName, list);
                }
            }
            return characteristicsDictionary;
        }
    }
}
