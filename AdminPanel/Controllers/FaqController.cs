using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers;

public sealed class FaqController : Controller
{
    public IActionResult QueryStringFaq()
    {
        return View();
    }
}