using AdminPanel.Data;
using AdminPanel.Helpers;
using AdminPanel.Models.DTO;
using Humanizer;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NickBuhro.Translit;
using System;
using System.Drawing;
using System.Reflection.PortableExecutable;
using WebApplication2.Models;

namespace AdminPanel.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Policy = PoliciesNames.Administrator)]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;

        public ProductsController(IMediator mediator, ApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO productDto)
        {
            if(!ModelState.IsValid)
            {
                return View(productDto);
            }

            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price
            };
            var brand = await _context.Brands.Where(x => x.Name == productDto.Brand).FirstOrDefaultAsync();
            var model = await _context.Models.Where(x => x.Name == productDto.Model).FirstOrDefaultAsync();
            var category = await _context.Categories.Where(x => x.Name == productDto.Category).FirstOrDefaultAsync();
            if (brand is not null)
            {
                product.BrandId = brand.Id;
                product.Brand = brand;
            }
            if (model is not null)
            {
                product.Model = model;
                product.ModelId = model.Id;
            }
            if (category is not null)
            {
                product.CategoryId = category.Id;
                product.Category = category;
            }

            product.IsActive = false;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            // после получения id товара инициализируется артикул и история цен
            product.Code = product.Id + 1000000;

            product.PriceHistory = new List<PriceHistory>();
            var priceHistory = new PriceHistory
            {
                Date = DateTime.Now,
                Price = product.Price,
                ProductId = product.Id
            };
            product.PriceHistory.Add(priceHistory);

            product.Url = CreateUrl(product.Name, product.Code);

            await _context.SaveChangesAsync();

            return LocalRedirect($"/Products/EditProduct/{product.Id}");
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductDTO productDto)
        {
            if (!ModelState.IsValid) return BadRequest("Incorrect data was passed");

            var product = await _context.Products
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Include(x => x.Model)
                .Include(x => x.PriceHistory)
                
                .FirstAsync(x => x.Id == productDto.Id);

            if(product is null) return BadRequest("Product was not found");

            if (product.Brand.Name != productDto.Brand)
            {
                var br = await _context.Brands.FirstOrDefaultAsync(x => x.Name == productDto.Brand);
                if(br is null) return BadRequest("This brand does not exist");
            }
            if (product.Category.Name != productDto.Category)
            {
                var br = await _context.Categories.FirstOrDefaultAsync(x => x.Name == productDto.Category);
                if (br is null) return BadRequest("This category does not exist");
            }
            if (product.Model.Name != productDto.Model)
            {
                var br = await _context.Models.FirstOrDefaultAsync(x => x.Name == productDto.Model);
                if (br is null) return BadRequest("This model does not exist");
            }

            
            if (product.Name != productDto.Name)
            {
                product.Name = productDto.Name;
                product.Url = CreateUrl(product.Name, product.Code);
            }

            if (product.Price != productDto.Price)
            {
                product.Price = productDto.Price;
                var priceHistory = new PriceHistory
                {
                    Date = DateTime.Now,
                    Price = product.Price,
                    ProductId = product.Id
                };
                product.PriceHistory?.Add(priceHistory);
            }
            
            product.Description = productDto.Description;
            product.DiscountAmmount = productDto.DiscountAmount;

            await _context.SaveChangesAsync();

            return LocalRedirect($"/Products/EditProduct/{product.Id}");
        }

        public async Task<IActionResult> ChangeProductIsActiveStatus(int productId, bool isActive)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (product is null) return BadRequest("Product was not found");
            product.IsActive = isActive;
            await _context.SaveChangesAsync();
            return LocalRedirect($"/Products/EditProduct/{product.Id}");
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _context.Products
                .Include(x => x.Characteristics)
                    .ThenInclude(x => x.CharacteristicsName)
                .Include(x => x.Characteristics)
                    .ThenInclude(x => x.CharacteristicsValue)
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Include(x => x.Model)
                .Include(x => x.Images)
                .Include(x => x.PriceHistory)
                .SingleOrDefaultAsync(x => x.Id == id);

            if(product is null)
            {
                return NotFound(product);
            }
            return View(product);
        }

        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> EditCharacteristic(int productId, int id, string name, string value)
        {
            var product = await _context.Products
                .Include(x => x.Characteristics)
                    .ThenInclude(x => x.CharacteristicsName)
                .Include(x => x.Characteristics)
                    .ThenInclude(x => x.CharacteristicsValue)
                .FirstOrDefaultAsync(x => x.Id == productId);
            if (product is null) return BadRequest("No product with given Id was found");

            var characteristic = await _context.Characteristics.FirstOrDefaultAsync(x => x.Id == id);
            if (characteristic is null) return BadRequest("There is no characteristic with given Id");

            var productCharacteristic = product.Characteristics.FirstOrDefault(x => x.CharacteristicsName.Name == name);
            if (productCharacteristic is not null && productCharacteristic.Id != id)
            {
                return BadRequest("This product already has characteristic with given name");
            }
            
            var characteristicName = await _context.CharacteristicsNames.FirstOrDefaultAsync(x => x.Name == name);
            var characteristicValue = await _context.CharacteristicsValues.FirstOrDefaultAsync(x => x.Value == value);
            if (characteristicName is null)
            {
                characteristicName = new CharacteristicName() { Name = name };
                await _context.CharacteristicsNames.AddAsync(characteristicName);
            }
            if (characteristicValue is null)
            {
                characteristicValue = new CharacteristicValue() { Value = value };
                await _context.CharacteristicsValues.AddAsync(characteristicValue);
            }
            product.Characteristics.First(x => x.Id == id).CharacteristicsName = characteristicName;
            product.Characteristics.First(x => x.Id == id).CharacteristicsValue = characteristicValue;
            
            await _context.SaveChangesAsync();

            return LocalRedirect($"/Products/EditProduct/{productId}");
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteCharacteristic(int productId, int id)
        {
            var characteristic = await _context.Characteristics.FirstOrDefaultAsync(x => x.Id == id);
            if (characteristic is null) return BadRequest("There is no characteristic with given Id");
            _context.Characteristics.Remove(characteristic);
            await _context.SaveChangesAsync();
            return LocalRedirect($"/Products/EditProduct/{productId}");
        }


        [HttpPost]
        public async Task<IActionResult> AddCharacteristic(int productId, string name, string value)
        {
            var characteristicsName = await _context.CharacteristicsNames.FirstOrDefaultAsync(x => x.Name == name);
            var characteristicsValue = await _context.CharacteristicsValues.FirstOrDefaultAsync(x => x.Value == value);
            

            bool isNewCharacteristic = false;
            if(characteristicsName is null)
            {
                characteristicsName = new CharacteristicName() { Name = name };
                await _context.CharacteristicsNames.AddAsync(characteristicsName);
                isNewCharacteristic = true;
            }
            if (characteristicsValue is null)
            {
                characteristicsValue = new CharacteristicValue() { Value = value };
                await _context.CharacteristicsValues.AddAsync(characteristicsValue);
                isNewCharacteristic = true;
            }
            if(isNewCharacteristic)
            {
                await _context.SaveChangesAsync();
            }


            var product = await _context.Products
                .Include(x => x.Characteristics)
                .ThenInclude(x => x.CharacteristicsName)
                .FirstOrDefaultAsync(x => x.Id == productId);
            if (product is null)
            {
                return BadRequest("Product with this id does not exist");
            }
            if (product.Characteristics.FirstOrDefault(x => x.CharacteristicsNameId == characteristicsName.Id) is not null)
            {
                return BadRequest("This product already has characteristic with this name");
            }

            
            var characteristic = new Characteristic()
            {
                CharacteristicsNameId = characteristicsName.Id,
                CharacteristicsValueId = characteristicsValue.Id,
                ProductId = productId
            };
            await _context.Characteristics.AddAsync(characteristic);
            await _context.SaveChangesAsync();

            return LocalRedirect($"/Products/EditProduct/{productId}");
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

        [HttpPost]
        public async Task<IActionResult> CreateBrand(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
            return View(brand);
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel(Model model)
        {
            await _context.Models.AddAsync(model);
            await _context.SaveChangesAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return View(category);
        }

        public IActionResult GetAutocompleteBrands(string input)
        {
            var items = _context.Brands.Where(x => x.Name.Contains(input)).Take(5);
            return Content(string.Join("", items.Select(item => $"<option class='autocomplete-item'>{item.Name}</option>")));
        }

        public IActionResult GetAutocompleteCategories(string input)
        {
            var items = _context.Categories.Where(x => x.Name.Contains(input)).Take(5);
            return Content(string.Join("", items.Select(item => $"<option class='autocomplete-item'>{item.Name}</option>")));
        }

        public IActionResult GetAutocompleteModels(string input)
        {
            var items = _context.Models.Where(x => x.Name.Contains(input)).Take(5);
            return Content(string.Join("", items.Select(item => $"<option class='autocomplete-item'>{item.Name}</option>")));
        }

        private string CreateUrl(string name, int code)
        {
            string url = name
                    .ToLower()
                    .Replace(" ", "-")
                    .Replace(".", "-")
                    .Replace("/", "-")
                    .Replace("\\", "-")
                    .Replace("(", "-")
                    .Replace(")", "-")
                    .Replace("ґ", "г")
                    .Replace("є", "е")
                    .Replace("і", "и")
                    .Replace("ї", "йи")
                    .Replace("[", "-")
                    .Replace("]", "-")
                    .Replace("{", "-")
                    .Replace("}", "-")
                    .Replace("?", "-")
                    .Replace("!", "-")
                    .Replace("----", "-")
                    .Replace("---", "-")
                    .Replace("--", "-")
                    .Dasherize();
            url = Transliteration.CyrillicToLatin(url)
                .Replace("`", "");

            if (url.Last() != '-') url += "-";
            url += $"{code}";

            return url;
        }
    }
}
