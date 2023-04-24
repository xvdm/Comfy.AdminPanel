using AdminPanel.MediatorHandlers.Banners;
using AdminPanel.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class BannersController : Controller
    {
        private readonly IMediator _mediator;

        public BannersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new GetBannersQuery());
            return View(result);
        }

        public async Task<IActionResult> EditBanner(Banner banner)
        {
            return View(banner);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBanner(string name, string imageUrl, string pageUrl)
        {
            await _mediator.Send(new CreateBannerCommand(name, imageUrl, pageUrl));
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBanner(int id)
        {
            await _mediator.Send(new DeleteBannerCommand(id));
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBanner(int id, string name, string imageUrl, string pageUrl)
        {
            await _mediator.Send(new EditBannerCommand(id, name, imageUrl, pageUrl));
            return RedirectToAction(nameof(Index));
        }
    }
}
