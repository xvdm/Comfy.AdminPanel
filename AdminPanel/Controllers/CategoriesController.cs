using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Products.Categories;
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
            return View(mainCategories);
        }

        public async Task<IActionResult> Category(int id)
        {
            var subcategories = await _mediator.Send(new GetSubcategoriesQuery(id));
            return View(subcategories);
        }
    }
}
