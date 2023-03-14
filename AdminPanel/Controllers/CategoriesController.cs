using AdminPanel.Handlers.Products.Models;
using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Products.Categories;
using AdminPanel.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Policy = PoliciesNames.Administrator)]
    public class CategoriesController : Controller
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var mainCategories = await _mediator.Send(new GetMainCategoriesQuery());
            var subcategories = await _mediator.Send(new GetAllSubcategoriesQuery());
            var viewModel = new CategoriesViewModel() { MainCategories = mainCategories, Subcategories = subcategories };
            return View(viewModel);
        }

        public async Task<IActionResult> Category(int id)
        {
            var subcategories = await _mediator.Send(new GetSubcategoriesQuery(id));
            return View(subcategories);
        }


        public IActionResult GetSubcategoriesForMainCategory(string mainCategoryId)
        {
            Console.WriteLine($"{mainCategoryId}");

            if(int.TryParse(mainCategoryId, out int categoryId) == false)
            {
                return BadRequest($"{mainCategoryId} is not int");
            }
            var items = _mediator.Send(new GetSubcategoriesForMainCategoryQuery(categoryId));
            return Content(string.Join("", items.Result.Select(item => $"<option class='autocomplete-item'>{item.Name}</option>")));
        }
    }
}
