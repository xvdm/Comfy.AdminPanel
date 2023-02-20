using AdminPanel.Data;
using AdminPanel.Helpers;
using AdminPanel.Models.DTO;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication2.Models;

namespace AdminPanel.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Policy = PoliciesNames.Administrator)]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(IMediator mediator, ApplicationDbContext context, IMapper mapper)
        {
            _mediator = mediator;
            _context = context;
            _mapper = mapper;
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
            if(ModelState.IsValid)
            {
                if(await _context.Products.FirstOrDefaultAsync(x => x.Name == productDto.Name) is not null)
                {
                    return View(productDto); // todo: return error: duplicate entry of Name
                }
                var product = new Product();
                product.Name = productDto.Name;
                product.Price = productDto.Price;
                var brand = await _context.Brands.Where(x => x.Name == productDto.Brand).FirstOrDefaultAsync();
                var model = await _context.Models.Where(x => x.Name == productDto.Model).FirstOrDefaultAsync();
                var category = await _context.Categories.Where(x => x.Name == productDto.Category).FirstOrDefaultAsync();
                if (brand is not null)
                { 
                    product.BrandId = brand.Id;
                    product.Brand = brand;
                }
                if(model is not null)
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
            }
            return View(productDto);
        }

        public IActionResult CreateBrand()
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
    }
}
