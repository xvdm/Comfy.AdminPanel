using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Characteristics;
using AdminPanel.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers;

[AutoValidateAntiforgeryToken]
[Authorize(Policy = RoleNames.Administrator)]
public sealed class CharacteristicsController : Controller
{
    private readonly IMediator _mediator;

    public CharacteristicsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> CharacteristicNames(string? searchString, int? pageSize, int? pageNumber)
    {
        var query = new GetCharacteristicNamesQuery(searchString, pageSize, pageNumber);
        var names = await _mediator.Send(query);
        var totalCount = await _mediator.Send(new GetCharacteristicNamesTotalCountQuery());
        var totalPages = (totalCount - 1) / query.PageSize + 1;
        var viewModel = new CharacteristicNamesViewModel
        {
            Names = names,
            TotalPages = totalPages,
            CurrentPage = query.PageNumber
        };
        return View(viewModel);
    }

    public async Task<IActionResult> CharacteristicValues(string? searchString, int? pageSize, int? pageNumber)
    {
        var query = new GetCharacteristicValuesQuery(searchString, pageSize, pageNumber);
        var values = await _mediator.Send(query);
        var totalCount = await _mediator.Send(new GetCharacteristicValuesTotalCountQuery());
        var totalPages = (totalCount - 1) / query.PageSize + 1;
        var viewModel = new CharacteristicValuesViewModel
        {
            Values = values,
            TotalPages = totalPages,
            CurrentPage = query.PageNumber
        };
        return View(viewModel);
    }
}