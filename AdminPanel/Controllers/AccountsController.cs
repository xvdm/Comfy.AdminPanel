using AdminPanel.Data;
using AdminPanel.Helpers;
using AdminPanel.Models;
using AdminPanel.Models.DTO;
using AdminPanel.Models.Logging;
using AdminPanel.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace AdminPanel.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Policy = PoliciesNames.Manager)]
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly DTOService _DTOService;
        private readonly DatabaseLoggerService _databaseLoggerService;

        public AccountsController(
            UserManager<ApplicationUser> userManager, 
            ApplicationDbContext context, 
            DTOService DTOService, 
            DatabaseLoggerService databaseLoggerService)
        {
            _userManager = userManager;
            _context = context;
            _DTOService = DTOService;
            _databaseLoggerService = databaseLoggerService;
        }

        public IActionResult Index()
        {
            
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id.ToString());
                if (user == null)
                {
                    return NotFound($"No user with ID '{model.Id}'.");
                }

                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                await _userManager.UpdateAsync(user);

                if (user.UserName != model.UserName)
                {
                    await _userManager.UpdateNormalizedUserNameAsync(user);
                }
                if (user.Email != model.Email)
                {
                    await _userManager.UpdateNormalizedEmailAsync(user);
                }
                await _userManager.UpdateSecurityStampAsync(user);
                await _context.SaveChangesAsync();
                await _databaseLoggerService.LogUserAction(User, user.Id, LoggingActionNames.Update);
            }

            var users = _DTOService.GetActiveDTOUsers();
            return View("Index", users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = PoliciesNames.SeniorManager)]
        public async Task<IActionResult> CreateUser(CreateUserDTO model)
        {
            if(ModelState.IsValid)
            {
                var user = model.Adapt<ApplicationUser>();
                var result = await _userManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, model.Role));
                    await _databaseLoggerService.LogUserAction(User, user.Id, LoggingActionNames.Create);
                }
            }
            return View("Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> LockoutUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                if (User.Identity?.Name != user.UserName)
                {
                    await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Parse("2999-03-01"));
                    await _databaseLoggerService.LogUserAction(User, id, LoggingActionNames.Lockout);
                }
            }
            var users = _DTOService.GetActiveDTOUsers();
            return View("ActiveUsers", users);
        }

        [HttpPost]
        public async Task<IActionResult> ActivateUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                if (User.Identity?.Name != user.UserName)
                {
                    await _userManager.SetLockoutEndDateAsync(user, null);
                    await _databaseLoggerService.LogUserAction(User, id, LoggingActionNames.Activate);
                }
            }
            var users = _DTOService.GetLockoutedDTOUsers();
            return View("LockoutedUsers", users);
        }

        public IActionResult ShowActiveUsers()
        {
            var users = _DTOService.GetActiveDTOUsers();
            return View("ActiveUsers", users);
        }

        public IActionResult ShowLockoutedUsers()
        {
            var users = _DTOService.GetLockoutedDTOUsers();
            return View("LockoutedUsers", users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
