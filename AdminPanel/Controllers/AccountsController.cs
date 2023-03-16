using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdminPanel.Models.DTO;
using AdminPanel.Helpers;
using Mapster;
using MediatR;
using AdminPanel.Handlers.Users;
using AdminPanel.Handlers.Logging;

namespace AdminPanel.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Policy = PoliciesNames.Manager)]
    public class AccountsController : Controller
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> ActiveUsers(string? searchString)
        {
            var users = await _mediator.Send(new GetDTOUsersQuery(searchString, false));
            return View(users);
        }

        public async Task<IActionResult> LockoutedUsers(string? searchString)
        {
            var users = await _mediator.Send(new GetDTOUsersQuery(searchString, true));
            return View(users);
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
            return View(nameof(Index));
        }

        [HttpPost]
        [Authorize(Policy = PoliciesNames.SeniorManager)]
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
            var lockoutUserCommand = new ChangeUserLockoutStatusCommand(User, id, true);
            if (await _mediator.Send(lockoutUserCommand))
            {
                var createUserLogCommand = new CreateUserLogCommand(User, id, LoggingActionNames.Lockout);
                await _mediator.Send(createUserLogCommand);
            }

            var users = await _mediator.Send(new GetDTOUsersQuery(searchString, false));
            return View(nameof(ActiveUsers), users);
        }

        [HttpPost]
        public async Task<IActionResult> ActivateUser(Guid id, string? searchString)
        {
            var activateUserCommand = new ChangeUserLockoutStatusCommand(User, id, false);
            if (await _mediator.Send(activateUserCommand))
            {
                var createUserLogCommand = new CreateUserLogCommand(User, id, LoggingActionNames.Activate);
                await _mediator.Send(createUserLogCommand);
            }

            var users = await _mediator.Send(new GetDTOUsersQuery(searchString, true));
            return View(nameof(LockoutedUsers), users);
        }
    }
}
