using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Categories;
using AdminPanel.MediatorHandlers.Products.Categories;
using AdminPanel.MediatorHandlers.Products.Images;
using AdminPanel.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers;


[AutoValidateAntiforgeryToken]
[Authorize(Policy = RoleNames.Administrator)]
public sealed class CategoriesController : Controller
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
        var viewModel = new CategoriesViewModel { MainCategories = mainCategories, Subcategories = subcategories };
        return View(viewModel);
    }

    public async Task<IActionResult> Category(int id)
    {
        var subcategories = await _mediator.Send(new GetSubcategoriesQuery(id));
        return View(subcategories);
    }

    public async Task<IActionResult> SubcategoryFilters(string? searchString)
    {
        var subcategoryFilters = await _mediator.Send(new GetSubcategoryFiltersQuery());
        return View(subcategoryFilters);
    }

    public async Task<IActionResult> GetMainCategoryImageUrl(string id)
    {
        if (int.TryParse(id, out var categoryId) == false)
        {
            return BadRequest("GetMainCategoryImageUrl :: Parsing error :: id");
        }
        var imageUrl = await _mediator.Send(new GetMainCategoryImageUrlQuery(categoryId));
        return Ok(new { ImageUrl = imageUrl });
    }

    public async Task<IActionResult> GetSubcategoryImageUrl(string id)
    {
        if (int.TryParse(id, out var categoryId) == false)
        {
            return BadRequest("GetSubcategoryImageUrl :: Parsing error :: id");
        }
        var imageUrl = await _mediator.Send(new GetSubcategoryImageUrlQuery(categoryId));
        return Ok(new { ImageUrl = imageUrl });
    }

    [HttpPost]
    public async Task<IActionResult> AddImageToCategory(IFormFile image, string categoryId, bool isSubcategory)
    {
        if (int.TryParse(categoryId, out var categoryIdInt) == false)
        {
            return BadRequest("AddImageToCategory :: Parsing error :: categoryId");
        }
        if (isSubcategory)
        {
            await _mediator.Send(new UpdateSubcategoryImageCommand(categoryIdInt, image));
        }
        else
        {
            await _mediator.Send(new UpdateMainCategoryImageCommand(categoryIdInt, image));
        }
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> RemoveImageFromCategory(string categoryId, bool isSubcategory)
    {
        if (int.TryParse(categoryId, out var categoryIdInt) == false)
        {
            return BadRequest("RemoveImageFromCategory :: Parsing error :: categoryId");
        }
        if (isSubcategory)
        {
            await _mediator.Send(new RemoveSubcategoryImageCommand(categoryIdInt));
        }
        else
        {
            await _mediator.Send(new RemoveMainCategoryImageCommand(categoryIdInt));
        }
        return Ok();
    }

    [HttpPost]
    [Authorize(Policy = RoleNames.SeniorAdministrator)]
    public async Task<IActionResult> AddSubcategoryFilter(int subcategoryId, string subcategoryFilterName, string subcategoryFilter)
    {
        await _mediator.Send(new CreateSubcategoryFilterCommand(subcategoryId, subcategoryFilterName, subcategoryFilter));
        return RedirectToAction(nameof(SubcategoryFilters));
    }
    

    [HttpPost]
    [Authorize(Policy = RoleNames.SeniorAdministrator)]
    public async Task<IActionResult> DeleteSubcategoryFilter(int id)
    {
        await _mediator.Send(new DeleteSubcategoryFilterCommand(id));
        return RedirectToAction(nameof(SubcategoryFilters));
    }

    public async Task<IActionResult> GetSubcategoriesForMainCategory(string mainCategoryId)
    {
        Console.WriteLine($"{mainCategoryId}");

        if(int.TryParse(mainCategoryId, out var categoryId) == false)
        {
            return BadRequest($"GetSubcategoriesForMainCategory :: Parsing error :: mainCategoryId");
        }
        var items = await _mediator.Send(new GetSubcategoriesForMainCategoryQuery(categoryId));
        var result = string.Join("", items.Select(item => $"<option value='{item.Id}' class='autocomplete-item'>{item.Name}</option>"));
        return Content(result);
    }

    [Authorize(Policy = RoleNames.SeniorAdministrator)]
    public async Task<IActionResult> EditMainCategoryName(string id, string name)
    {
        if (int.TryParse(id, out var categoryId) == false)
        {
            return BadRequest($"EditMainCategoryName :: Parsing error :: id");
        }
        await _mediator.Send(new UpdateMainCategoryCommand(categoryId, name));
        return Ok();
    }

    [Authorize(Policy = RoleNames.SeniorAdministrator)]
    public async Task<IActionResult> EditSubcategoryName(string id, string name)
    {
        if (int.TryParse(id, out var categoryId) == false)
        {
            return BadRequest($"EditSubcategoryName :: Parsing error :: id");
        }
        await _mediator.Send(new UpdateSubcategoryCommand(categoryId, name));
        return Ok();
    }

    [Authorize(Policy = RoleNames.SeniorAdministrator)]
    public async Task<IActionResult> DeleteMainCategory(string id)
    {
        if (int.TryParse(id, out var categoryId) == false)
        {
            return BadRequest($"DeleteMainCategory :: Parsing error :: id");
        }
        var result = await _mediator.Send(new DeleteMainCategoryCommand(categoryId));
        if (result) return Ok();
        return BadRequest("Main category still has subcategories");
    }

    [Authorize(Policy = RoleNames.SeniorAdministrator)]
    public async Task<IActionResult> DeleteSubcategory(string id)
    {
        if (int.TryParse(id, out var categoryId) == false)
        {
            return BadRequest($"DeleteMainCategory :: Parsing error :: id");
        }
        var result = await _mediator.Send(new DeleteSubcategoryCommand(categoryId));
        if (result) return Ok();
        return BadRequest("Subcategory still has products");
    }
}