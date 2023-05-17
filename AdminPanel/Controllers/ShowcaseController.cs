using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Showcase;
using AdminPanel.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers;


[AutoValidateAntiforgeryToken]
[Authorize(Policy = PoliciesNames.Administrator)]
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

    [Authorize(Policy = PoliciesNames.SeniorAdministrator)]
    public IActionResult EditGroup(int groupId, string name, string queryString)
    {
        var group = new ShowcaseGroup()
        {
            Id = groupId,
            Name = name,
            QueryString = queryString
        };
        return View(group);
    }

    [HttpPost]
    [Authorize(Policy = PoliciesNames.SeniorAdministrator)]
    public async Task<IActionResult> AddProduct(int groupId, int productCode)
    {
        await _mediator.Send(new AddProductToShowcaseCommand(groupId, productCode));
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Policy = PoliciesNames.SeniorAdministrator)]
    public async Task<IActionResult> RemoveProduct(int groupId, int productId)
    {
        await _mediator.Send(new DeleteProductFromShowcaseCommand(groupId, productId));
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Policy = PoliciesNames.SeniorAdministrator)]
    public async Task<IActionResult> AddGroup(string name, string queryString)
    {
        await _mediator.Send(new CreateShowcaseGroupCommand(name, queryString));
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Policy = PoliciesNames.SeniorAdministrator)]
    public async Task<IActionResult> RemoveGroup(int groupId)
    {
        await _mediator.Send(new DeleteShowcaseGroupCommand(groupId));
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Policy = PoliciesNames.SeniorAdministrator)]
    public async Task<IActionResult> UpdateGroup(int groupId, string name, string queryString)
    {
        await _mediator.Send(new UpdateShowcaseGroupCommand(groupId, name, queryString));
        return RedirectToAction(nameof(Index));
    }
}