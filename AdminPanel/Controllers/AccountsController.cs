using AdminPanel.Data;
using AdminPanel.Helpers;
using AdminPanel.Models;
using AdminPanel.Models.DTO;
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
        private readonly ILogger<AccountsController> _logger;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly DTOService _DTOService;

        public AccountsController(IUserService userService, ILogger<AccountsController> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext context, DTOService DTOService)
        {
            _logger = logger;
            _userService = userService;
            _userManager = userManager;
            _context = context;
            _DTOService = DTOService;
        }

        public IActionResult Index()
        {
            var users = _DTOService.GetDTOUsers();
            return View("Index", users);
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
            }

            var users = _DTOService.GetDTOUsers();
            return View("Index", users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                if(User.Identity?.Name != user.UserName)
                {
                    await _userManager.DeleteAsync(user);
                }
            }
            var users = _DTOService.GetDTOUsers();
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
                }
            }
            return View("Create", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
