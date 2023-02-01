using AdminPanel.Data;
using AdminPanel.Helpers;
using AdminPanel.Models;
using AdminPanel.Models.DTO;
using AdminPanel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminPanel.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly DTOService _DTOService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, UserManager<ApplicationUser> userManager, ApplicationDbContext context, DTOService DTOcontroller)
        {
            _logger = logger;
            _userService = userService;
            _userManager = userManager;
            _context = context;
            _DTOService = DTOcontroller;
        }

        [Authorize(Policy = RolesNames.Administrator)]
        public IActionResult AdminPanel()
        {
            return View();
        }

        [Authorize(Policy = RolesNames.Manager)]
        public IActionResult Accounts()
        {
            var users = _DTOService.GetDTOUsers();
            return View(users);
        }

        public string Allo()
        {
            return "adas";
        }

        [Authorize(Policy = RolesNames.Manager)]
        public IActionResult Logs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [Authorize(Policy=RolesNames.Manager)]
        public async Task<IActionResult> UpdateUser(UserDTO model)
        {
            if(!ModelState.IsValid)
            {
                var users = _DTOService.GetDTOUsers();
                return View("Accounts", users);
            }

            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            if(user == null)
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
            if(user.Email != model.Email)
            {
                await _userManager.UpdateNormalizedEmailAsync(user);
            }
            await _userManager.UpdateSecurityStampAsync(user);
            await _context.SaveChangesAsync();

            return Redirect("Accounts");
        }

        [HttpPost]
        [Authorize(Policy = RolesNames.Manager)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if(user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return Redirect("Accounts");
        }
    }
}