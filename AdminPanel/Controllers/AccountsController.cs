using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdminPanel.Models.DTO;
using AdminPanel.Helpers;
using Mapster;
using MediatR;
using AdminPanel.MediatorHandlers.Logging;
using AdminPanel.MediatorHandlers.Users;
using AdminPanel.Models.ViewModels;
using System.Drawing.Printing;

namespace AdminPanel.Controllers;


[AutoValidateAntiforgeryToken]
[Authorize(Policy = RoleNames.SeniorAdministrator)]
public sealed class AccountsController : Controller
{
    private readonly IMediator _mediator;

    public AccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IActionResult Create()
    {
        return View();
    }

    public async Task<IActionResult> ActiveUsers(string? searchString, int? pageSize, int? pageNumber)
    {
        bool lockout = false;
        var query = new GetDTOUsersQuery(searchString, lockout, pageSize, pageNumber);
        var users = await _mediator.Send(query);
        var totalCount = await _mediator.Send(new GetUsersTotalCountQuery(searchString, lockout));
        var totalPages = (totalCount - 1) / query.PageSize + 1;
        var viewModel = new UsersViewModel
        {
            Users = users,
            TotalPages = totalPages,
            CurrentPage = query.PageNumber
        };
        return View(viewModel);
    }

    public async Task<IActionResult> LockoutUsers(string? searchString, int? pageSize, int? pageNumber)
    {
        bool lockout = true;
        var query = new GetDTOUsersQuery(searchString, lockout, pageSize, pageNumber);
        var users = await _mediator.Send(query);
        var totalCount = await _mediator.Send(new GetUsersTotalCountQuery(searchString, lockout));
        var totalPages = (totalCount - 1) / query.PageSize + 1;
        var viewModel = new UsersViewModel
        {
            Users = users,
            TotalPages = totalPages,
            CurrentPage = query.PageNumber
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUser(UserDTO model)
    {
        if (ModelState.IsValid)
        {
            if(await _mediator.Send(model.Adapt<UpdateUserCommand>()))
            {
                var createUserLogCommand = new CreateUserLogCommand(User, model.Id, LoggingActionNames.Update);
                await _mediator.Send(createUserLogCommand);
            }
        }
        return RedirectToAction(nameof(ActiveUsers));
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserDTO model)
    {
        if(ModelState.IsValid)
        {
            var id = await _mediator.Send(model.Adapt<CreateUserCommand>());
            if(id != Guid.Empty)
            {
                var createUserLogCommand = new CreateUserLogCommand(User, id, LoggingActionNames.Create);
                await _mediator.Send(createUserLogCommand);
            }
        }
        return View(nameof(Create), model);
    }

    [HttpPost]
    public async Task<IActionResult> LockoutUser(Guid id, string? searchString)
    {
        var lockoutUserCommand = new UpdateUserLockoutStatusCommand(User, id, true);
        if (await _mediator.Send(lockoutUserCommand))
        {
            var createUserLogCommand = new CreateUserLogCommand(User, id, LoggingActionNames.Lockout);
            await _mediator.Send(createUserLogCommand);
        }
        return RedirectToAction(nameof(ActiveUsers));
    }

    [HttpPost]
    public async Task<IActionResult> ActivateUser(Guid id, string? searchString)
    {
        var activateUserCommand = new UpdateUserLockoutStatusCommand(User, id, false);
        if (await _mediator.Send(activateUserCommand))
        {
            var createUserLogCommand = new CreateUserLogCommand(User, id, LoggingActionNames.Activate);
            await _mediator.Send(createUserLogCommand);
        }
        return RedirectToAction(nameof(LockoutUsers));
    }
}