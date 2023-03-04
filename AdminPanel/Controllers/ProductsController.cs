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
using AdminPanel.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using System;
using System.Linq;

namespace AdminPanel.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;

        public ProductsController(IMediator mediator, ApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        public async Task<IActionResult> Products(int categoryId, string filterQuery)
        {
            //товары в подкатегории
            Console.WriteLine(filterQuery);
            Console.WriteLine(filterQuery);
            Console.WriteLine(filterQuery);
            Console.WriteLine(filterQuery);
            Console.WriteLine(filterQuery);
            if (ProductUrl.TryRemoveEmptyAndDuplicatesFromQuery(filterQuery, out Dictionary<string, List<string>> queryDictionary))
            {
                return LocalRedirect($"/Products/Products?categoryId={categoryId}&filterQuery={WebUtility.UrlEncode(ProductUrl.GetUrlQuery(queryDictionary))}");
            }

            var products = await _mediator.Send(new GetProductsQuery(categoryId, 0, 10, queryDictionary));
            var brands = await _mediator.Send(new GetBrandsQuery(categoryId));

            var category = await _context.Subcategories
                .Include(x => x.UniqueCharacteristics)
                    .ThenInclude(x => x.CharacteristicsName)
                .Include(x => x.UniqueCharacteristics)
                    .ThenInclude(x => x.CharacteristicsValue)
                .Where(x => x.Id == categoryId)
                .FirstOrDefaultAsync();

            var characteristicsDictionary = await GetCharacteristicsDictionary(category);

            var viewModel = new ProductsViewModel()
            {
                CategoryId = category.Id,
                Characteristics = characteristicsDictionary,
                Query = filterQuery,
                Products = products,
                Brands = brands
            };

            return View(viewModel);
        }

        private async Task<Dictionary<CharacteristicName, List<CharacteristicValue>>> GetCharacteristicsDictionary(Subcategory category)
        {
            var characteristicsDictionary = new Dictionary<CharacteristicName, List<CharacteristicValue>>();

            foreach (var characteristic in category.UniqueCharacteristics)
            {
                if(characteristicsDictionary.TryGetValue(characteristic.CharacteristicsName, out List<CharacteristicValue>? characteristicValues))
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
            var addCharacteristic = new AddProductCharacteristicCommand()
            {
                ProductId = productId,
                Name = name,
                Value = value
            };
            
            await _mediator.Send(addCharacteristic);

            return LocalRedirect($"/Products/EditProduct/{productId}");
        }
    }
}
