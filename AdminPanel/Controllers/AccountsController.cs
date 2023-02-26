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
            var users = await _mediator.Send(new GetDTOUsersQuery(false));
            return View(users);
        }

        public async Task<IActionResult> LockoutedUsers()
        {
            var users = await _mediator.Send(new GetDTOUsersQuery(true));
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserDTO model)
        {
            if (ModelState.IsValid)
            {
                if(await _mediator.Send(model.Adapt<UpdateUserCommand>()))
                {
                    var createUserLogCommand = new CreateUserLogCommand() { 
                        User = User, 
                        SubjetUserId = model.Id, 
                        Action = LoggingActionNames.Update 
                    };
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
                var createUserCommandResult = await _mediator.Send(model.Adapt<CreateUserCommand>());
                if(createUserCommandResult != Guid.Empty)
                {
                    var createUserLogCommand = new CreateUserLogCommand() { 
                        User = User, 
                        SubjetUserId = createUserCommandResult, 
                        Action = LoggingActionNames.Create 
                    };
                    await _mediator.Send(createUserLogCommand);
                }
            }
            return View(nameof(Create), model);
        }

        [HttpPost]
        public async Task<IActionResult> LockoutUser(Guid id)
        {
            var lockoutUserCommand = new ChangeUserLockoutStatusCommand() { 
                CurrentUser = User, 
                UserId = id, 
                IsLockout = true 
            };
            if (await _mediator.Send(lockoutUserCommand))
            {
                var createUserLogCommand = new CreateUserLogCommand() { 
                    User = User, 
                    SubjetUserId = id, 
                    Action = LoggingActionNames.Lockout 
                };
                await _mediator.Send(createUserLogCommand);
            }

            var users = await _mediator.Send(new GetDTOUsersQuery(false));
            return View(nameof(ActiveUsers), users);
        }

        [HttpPost]
        public async Task<IActionResult> ActivateUser(Guid id)
        {
            var activateUserCommand = new ChangeUserLockoutStatusCommand() { 
                CurrentUser = User, 
                UserId = id, 
                IsLockout = false 
            };
            if (await _mediator.Send(activateUserCommand))
            {
                var createUserLogCommand = new CreateUserLogCommand() { 
                    User = User, 
                    SubjetUserId = id, 
                    Action = LoggingActionNames.Activate 
                };
                await _mediator.Send(createUserLogCommand);
            }

            var users = await _mediator.Send(new GetDTOUsersQuery(true));
            return View(nameof(LockoutedUsers), users);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
