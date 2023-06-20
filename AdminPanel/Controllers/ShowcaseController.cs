using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Showcase;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers;


[AutoValidateAntiforgeryToken]
[Authorize(Policy = RoleNames.Administrator)]
public sealed class ShowcaseController : Controller
{
    private readonly IMediator _mediator;

    public ShowcaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var groups = await _mediator.Send(new GetShowcaseGroupsQuery());
        return View(groups);
    }

    [Authorize(Policy = RoleNames.SeniorAdministrator)]
    public IActionResult EditGroup(int groupId, string name, int subcategoryId, string? queryString)
    {
        var group = new ShowcaseGroup
        {
            Id = groupId,
            Name = name,
            QueryString = queryString,
            SubcategoryId = subcategoryId
        };
        return View(group);
    }

    [HttpPost]
    [Authorize(Policy = RoleNames.SeniorAdministrator)]
    public async Task<IActionResult> AddProduct(int groupId, int productCode)
    {
        await _mediator.Send(new AddProductToShowcaseCommand(groupId, productCode));
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Policy = RoleNames.SeniorAdministrator)]
    public async Task<IActionResult> RemoveProduct(int groupId, int productId)
    {
        await _mediator.Send(new DeleteProductFromShowcaseCommand(groupId, productId));
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Policy = RoleNames.SeniorAdministrator)]
    public async Task<IActionResult> AddGroup(string name, int subcategoryId, string? queryString)
    {
        await _mediator.Send(new CreateShowcaseGroupCommand(name, subcategoryId, queryString));
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Policy = RoleNames.SeniorAdministrator)]
    public async Task<IActionResult> RemoveGroup(int groupId)
    {
        await _mediator.Send(new DeleteShowcaseGroupCommand(groupId));
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Policy = RoleNames.SeniorAdministrator)]
    public async Task<IActionResult> UpdateGroup(int groupId, string name, int subcategoryId, string? queryString)
    {
        await _mediator.Send(new UpdateShowcaseGroupCommand(groupId, name, subcategoryId, queryString));
        return RedirectToAction(nameof(Index));
    }
}