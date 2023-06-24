using AdminPanel.MediatorHandlers.CharacteristicGroups;
using AdminPanel.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers;

public sealed class CharacteristicGroupsController : Controller
{
    private readonly ISender _sender;

    public CharacteristicGroupsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> AddGroup(int productId, string name)
    {
        await _sender.Send(new AddCharacteristicGroupCommand(productId, name));
        return LocalRedirect($"/Products/EditProduct/{productId}");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveGroup(int productId, int groupId)
    {
        await _sender.Send(new RemoveCharacteristicGroupCommand(groupId));
        return LocalRedirect($"/Products/EditProduct/{productId}");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateGroup(int productId, int groupId, string name)
    {
        await _sender.Send(new UpdateCharacteristicGroupCommand(groupId, name));
        return LocalRedirect($"/Products/EditProduct/{productId}");
    }

    public IActionResult EditGroup(int productId, int groupId, string name)
    {
        var group = new CharacteristicGroup
        {
            Id = groupId,
            Name = name,
            ProductId = productId
        };
        return View(group);
    }
}