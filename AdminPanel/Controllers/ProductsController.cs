using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Products;
using AdminPanel.MediatorHandlers.Products.Brands;
using AdminPanel.MediatorHandlers.Products.Categories;
using AdminPanel.MediatorHandlers.Products.Models;
using AdminPanel.Models.DTO;
using AdminPanel.Models.ViewModels;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers;


[AutoValidateAntiforgeryToken]
[Authorize(Policy = PoliciesNames.Administrator)]
public sealed class ProductsController : Controller
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> CreateProduct()
    {
        var viewModel = await GetMainCategoriesBrandsModelsViewModel();
        return View(viewModel);
    }

    public async Task<IActionResult> EditProduct(int id)
    {
        var viewModel = await GetProductCategoriesViewModel(id);
        if(viewModel.Product is null)
        {
            return NotFound(viewModel.Product);
        }
        return View(viewModel);
    }

    public async Task<IActionResult> Products(string? searchString, int? pageSize, int? pageNumber)
    {
        var products = await _mediator.Send(new GetProductsQuery(searchString, pageSize, pageNumber));
        var viewModel = new ProductsViewModel()
        {
            Products = products
        };
        return View(viewModel);
    }

    public async Task<IActionResult> ChangeProductActivityStatus(string productId, string isActive)
    {
        if (int.TryParse(productId, out var productIdInt) == false)
        {
            return BadRequest("ChangeProductActivityStatus :: productId :: parse to int error");
        }
        if (bool.TryParse(isActive, out var isActiveBool) == false)
        {
            return BadRequest("ChangeProductActivityStatus :: isActive :: parse to bool error");
        }
        await _mediator.Send(new UpdateProductActivityStatusCommand(productIdInt, isActiveBool));
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductDTO productDto)
    {
        if (!ModelState.IsValid)
        {
            var viewModel = await GetMainCategoriesBrandsModelsViewModel();
            return View(viewModel);
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
        var command = productDto.Adapt<UpdateProductCommand>();
        var productId = await _mediator.Send(command);
        return LocalRedirect($"/Products/EditProduct/{productId}");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var command = new DeleteProductCommand(id);
        await _mediator.Send(command);
        return RedirectToAction(nameof(Products));
    }

    [HttpPost]
    public async Task<IActionResult> EditCharacteristic(string productId, string id, string name, string value)
    {
        Console.WriteLine($"productId = {productId}");
        if (int.TryParse(productId, out var productIdInt) == false)
        {
            return BadRequest("EditCharacteristic :: productId :: parse to int error");
        }
        if (int.TryParse(id, out var idInt) == false)
        {
            return BadRequest("EditCharacteristic :: id :: parse to int error");
        }

        var editCharacteristicCommand = new UpdateProductCharacteristicCommand(productIdInt, idInt, name, value);
        await _mediator.Send(editCharacteristicCommand);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCharacteristic(string productId, string id)
    {
        if (int.TryParse(productId, out var productIdInt) == false)
        {
            return BadRequest("DeleteCharacteristic :: productId :: parse to int error");
        }
        if (int.TryParse(id, out var characteristicIdInt) == false)
        {
            return BadRequest("DeleteCharacteristic :: id :: parse to int error");
        }
        await _mediator.Send(new DeleteProductCharacteristicCommand(productIdInt, characteristicIdInt));
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddCharacteristic(string productId, string name, string value)
    {
        if (int.TryParse(productId, out var productIdInt) == false)
        {
            return BadRequest("AddCharacteristic :: productId :: parse to int error");
        }
        var addCharacteristic = new CreateProductCharacteristicCommand(productIdInt, name, value);
        var characteristic = await _mediator.Send(addCharacteristic);
        return Ok(characteristic);
    }

    private async Task<ProductCategoriesViewModel> GetProductCategoriesViewModel(int productId)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(productId));
        if(product is null)
        {
            throw new HttpRequestException($"No product with id {productId} was found");
        }
        var mainCategories = await _mediator.Send(new GetMainCategoriesQuery());
        var subcategories = await _mediator.Send(new GetSubcategoriesForMainCategoryQuery(product.Category.MainCategoryId));

        var models = await _mediator.Send(new GetModelsQuery(null, null));
        var brands = await _mediator.Send(new GetBrandsQuery(null, null));

        var viewModel = new ProductCategoriesViewModel()
        {
            Product = product,
            MainCategories = mainCategories,
            Subcategories = subcategories,
            Models = models.OrderBy(x => x.Name),
            Brands = brands.OrderBy(x => x.Name)
        };
        return viewModel;
    }

    private async Task<MainCategoriesBrandsModelsViewModel> GetMainCategoriesBrandsModelsViewModel()
    {
        var mainCategories = await _mediator.Send(new GetMainCategoriesQuery());
        var models = await _mediator.Send(new GetModelsQuery(null, null));
        var brands = await _mediator.Send(new GetBrandsQuery(null, null));

        var viewModel = new MainCategoriesBrandsModelsViewModel()
        {
            MainCategories = mainCategories,
            Models = models.OrderBy(x => x.Name),
            Brands = brands.OrderBy(x => x.Name)
        };
        return viewModel;
    }
}