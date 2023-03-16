﻿using AdminPanel.Handlers.Products.Models;
using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Products.Categories;
using AdminPanel.Models;
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
                return BadRequest($"GetSubcategoriesForMainCategory :: Parsing error :: mainCategoryId");
            }
            var items = _mediator.Send(new GetSubcategoriesForMainCategoryQuery(categoryId));
            string result = string.Join("", items.Result.Select(item => $"<option class='autocomplete-item'>{item.Name}</option>"));
            return Content(result);
        }

        public async Task<IActionResult> EditMainCategoryName(string id, string name)
        {
            if (int.TryParse(id, out int categoryId) == false)
            {
                return BadRequest($"EditMainCategoryName :: Parsing error :: id");
            }
            await _mediator.Send(new EditMainCategoryCommand(categoryId, name));
            return Ok();
        }

        public async Task<IActionResult> EditSubcategoryName(string id, string name)
        {
            if (int.TryParse(id, out int categoryId) == false)
            {
                return BadRequest($"EditSubcategoryName :: Parsing error :: id");
            }
            await _mediator.Send(new EditSubcategoryCommand(categoryId, name));
            return Ok();
        }

        public async Task<IActionResult> DeleteMainCategory(string id)
        {
            if (int.TryParse(id, out int categoryId) == false)
            {
                return BadRequest($"DeleteMainCategory :: Parsing error :: id");
            }
            var result = await _mediator.Send(new DeleteMainCategoryCommand(categoryId));
            if (result) return Ok();
            else return BadRequest("Main category still has subcategories");
        }

        public async Task<IActionResult> DeleteSubcategory(string id)
        {
            if (int.TryParse(id, out int categoryId) == false)
            {
                return BadRequest($"DeleteMainCategory :: Parsing error :: id");
            }
            var result = await _mediator.Send(new DeleteSubcategoryCommand(categoryId));
            if (result) return Ok();
            else return BadRequest("Subcategory still has products");
        }
    }
}
