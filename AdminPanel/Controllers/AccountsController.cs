using Microsoft.AspNetCore.Authorization;
using AdminPanel.Models.ViewModels;
using AdminPanel.Commands.Users;
using Microsoft.AspNetCore.Mvc;
using AdminPanel.Commands.Logs;
using AdminPanel.Queries.Users;
using AdminPanel.Models.DTO;
using System.Diagnostics;
using AdminPanel.Helpers;
using Mapster;
using MediatR;

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

        public async Task<IActionResult> ActiveUsers()
        {
            var query = new GetDTOUsersQuery(false);
            var result = await _mediator.Send(query);
            return View(result);
        }

        public async Task<IActionResult> LockoutedUsers()
        {
            var query = new GetDTOUsersQuery(true);
            var result = await _mediator.Send(query);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserDTO model)
        {
            if (ModelState.IsValid)
            {
                var updateUserCommand = model.Adapt<UpdateUserCommand>();
                var updateUserCommandResult = await _mediator.Send(updateUserCommand);
                if(updateUserCommandResult)
                {
                    var createUserLogCommand = new CreateUserLogCommand() { User = User, SubjetUserId = model.Id, Action = LoggingActionNames.Update };
                    var createUserLogCommandResult = await _mediator.Send(createUserLogCommand);
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
                var createUserCommand = model.Adapt<CreateUserCommand>();
                var createUserCommandResult = await _mediator.Send(createUserCommand);
                if(createUserCommandResult != Guid.Empty)
                {
                    var createUserLogCommand = new CreateUserLogCommand() { User = User, SubjetUserId = createUserCommandResult, Action = LoggingActionNames.Create };
                    var createUserLogCommandResult = await _mediator.Send(createUserLogCommand);
                }
            }
            return View(nameof(Create), model);
        }

        [HttpPost]
        public async Task<IActionResult> LockoutUser(Guid id)
        {
            var lockoutUserCommand = new ChangeUserLockoutStatusCommand() { CurrentUser = User, UserId = id, IsLockout = true };
            var lockoutUserCommandResult = await _mediator.Send(lockoutUserCommand);
            if (lockoutUserCommandResult)
            {
                var createUserLogCommand = new CreateUserLogCommand() { User = User, SubjetUserId = id, Action = LoggingActionNames.Lockout };
                var createUserLogCommandResult = await _mediator.Send(createUserLogCommand);
            }

            var query = new GetDTOUsersQuery(false);
            var result = await _mediator.Send(query);
            return View(nameof(ActiveUsers), result);
        }

        [HttpPost]
        public async Task<IActionResult> ActivateUser(Guid id)
        {
            var activateUserCommand = new ChangeUserLockoutStatusCommand() { CurrentUser = User, UserId = id, IsLockout = false };
            var activateUserCommandResult = await _mediator.Send(activateUserCommand);
            if (activateUserCommandResult)
            {
                var createUserLogCommand = new CreateUserLogCommand() { User = User, SubjetUserId = id, Action = LoggingActionNames.Activate };
                var createUserLogCommandResult = await _mediator.Send(createUserLogCommand);
            }

            var query = new GetDTOUsersQuery(true);
            var result = await _mediator.Send(query);
            return View(nameof(LockoutedUsers), result);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
