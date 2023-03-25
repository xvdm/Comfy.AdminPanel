﻿using AdminPanel.Handlers.Products.Brands;
using AdminPanel.Handlers.Products.Categories;
using AdminPanel.Handlers.Products.Models;
using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Products.Brands;
using AdminPanel.MediatorHandlers.Products.Models;
using AdminPanel.Models;
using AdminPanel.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> Brands(int? pageSize, int? pageNumber)
        {
            var query = new GetBrandsQuery(pageSize, pageNumber);
            var brands = await _mediator.Send(query);
            var totalCount = await _mediator.Send(new GetBrandsTotalCountQuery());
            var totalPages = totalCount / query.PageSize + 1;
            var viewModel = new BrandsViewModel()
            {
                Brands = brands,
                TotalPages = totalPages
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Models(int? pageSize, int? pageNumber)
        {
            var query = new GetModelsQuery(pageSize, pageNumber);
            var models = await _mediator.Send(query);
            var totalCount = await _mediator.Send(new GetModelsTotalCountQuery());
            var totalPages = totalCount / query.PageSize + 1;
            var viewModel = new ModelsViewModel()
            {
                Models = models,
                TotalPages = totalPages
            };
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateBrand(string brandId, string newName)
        {
            if (int.TryParse(brandId, out var brandIdInt) == false)
            {
                return BadRequest("UpdateBrand :: brandId :: parse to int error");
            }
            await _mediator.Send(new UpdateBrandCommand(brandIdInt, newName));
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateModel(string modelId, string newName)
        {
            if (int.TryParse(modelId, out var modelIdInt) == false)
            {
                return BadRequest("UpdateModel :: modelId :: parse to int error");
            }
            await _mediator.Send(new UpdateModelCommand(modelIdInt, newName));
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBrand(string brandId)
        {
            if (int.TryParse(brandId, out var brandIdInt) == false)
            {
                return BadRequest("UpdateBrand :: brandId :: parse to int error");
            }
            await _mediator.Send(new DeleteBrandCommand(brandIdInt));
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteModel(string modelId)
        {
            if (int.TryParse(modelId, out var modelIdInt) == false)
            {
                return BadRequest("UpdateModel :: modelId :: parse to int error");
            }
            await _mediator.Send(new DeleteModelCommand(modelIdInt));
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(Brand brand)
        {
            var result = await _mediator.Send(new CreateBrandCommand(brand));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel(Model model)
        {
            var result = await _mediator.Send(new CreateModelCommand(model));
            return Ok(result);
        }

        [Authorize(Policy = PoliciesNames.Owner)]
        [HttpPost]
        public async Task<IActionResult> CreateMainCategory(string name)
        {
            var category = new MainCategory()
            {
                Name = name
            };
            await _mediator.Send(new CreateMainCategoryCommand(category));
            return Ok();
        }

        [Authorize(Policy = PoliciesNames.Owner)]
        [HttpPost]
        public async Task<IActionResult> CreateSubcategory(string mainCategoryId, string name)
        {
            if (int.TryParse(mainCategoryId, out var mainCategoryIdInt) == false)
            {
                return BadRequest("CreateSubcategory :: mainCategoryId :: parse to int error");
            }
            var category = new Subcategory()
            {
                MainCategoryId = mainCategoryIdInt,
                Name = name
            };
            await _mediator.Send(new CreateSubcategoryCommand(category));
            return Ok();
        }

        public IActionResult GetAutocompleteBrands(string input)
        {
            var items = _mediator.Send(new GetAutocompleteBrandsQuery(input, 5));
            return Content(string.Join("", items.Result.Select(item => $"<option class='autocomplete-item'>{item.Name}</option>")));
        }

        public IActionResult GetAutocompleteCategories(string input)
        {
            var items = _mediator.Send(new GetAutocompleteSubcategoriesQuery(input, 5));
            return Content(string.Join("", items.Result.Select(item => $"<option value='{item.Id}' class='autocomplete-item'>{item.Name}</option>")));
        }

        public IActionResult GetAutocompleteModels(string input)
        {
            var items = _mediator.Send(new GetAutocompleteModelsQuery(input, 5));
            return Content(string.Join("", items.Result.Select(item => $"<option class='autocomplete-item'>{item.Name}</option>")));
        } 
    }
}
