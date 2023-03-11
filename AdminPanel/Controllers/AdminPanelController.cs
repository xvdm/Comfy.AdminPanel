using AdminPanel.Handlers.Products.Brands;
using AdminPanel.Handlers.Products.Categories;
using AdminPanel.Handlers.Products.Models;
using AdminPanel.Helpers;
using AdminPanel.Models;
using AdminPanel.Models.DTO;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

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

        public IActionResult Index()
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

        [Authorize(Policy = PoliciesNames.Owner)]
        public IActionResult CreateMainCategory()
        {
            return View();
        }

        [Authorize(Policy = PoliciesNames.Owner)]
        public IActionResult CreateSubcategory()
        {
            return View();
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

        [Authorize(Policy = PoliciesNames.Owner)]
        [HttpPost]
        public async Task<IActionResult> CreateMainCategory(CreateMainCategoryDTO categoryDTO)
        {
            var category = categoryDTO.Adapt<MainCategory>();
            await _mediator.Send(new CreateMainCategoryCommand(category));
            return View(categoryDTO);
        }

        [Authorize(Policy = PoliciesNames.Owner)]
        [HttpPost]
        public async Task<IActionResult> CreateSubcategory(CreateSubcategoryDTO categoryDTO)
        {
            var category = categoryDTO.Adapt<Subcategory>();
            await _mediator.Send(new CreateSubcategoryCommand(category));
            return View(categoryDTO);
        }

        public IActionResult GetAutocompleteBrands(string input)
        {
            var items = _mediator.Send(new GetAutocompleteBrandsQuery(input, 5));
            return Content(string.Join("", items.Result.Select(item => $"<option class='autocomplete-item'>{item.Name}</option>")));
        }

        public IActionResult GetAutocompleteCategories(string input)
        {
            var items = _mediator.Send(new GetAutocompleteSubcategoriesQuery(input, 5));
            return Content(string.Join("", items.Result.Select(item => $"<option class='autocomplete-item'>{item.Name}</option>")));
        }

        public IActionResult GetAutocompleteModels(string input)
        {
            var items = _mediator.Send(new GetAutocompleteModelsQuery(input, 5));
            return Content(string.Join("", items.Result.Select(item => $"<option class='autocomplete-item'>{item.Name}</option>")));
        } 
    }
}
