using AdminPanel.Data;
using AdminPanel.Handlers.Products;
using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Products.Brands;
using AdminPanel.MediatorHandlers.Products.Categories;
using AdminPanel.MediatorHandlers.Products.Models;
using AdminPanel.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AdminPanel.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Policy = PoliciesNames.Administrator)]
    public class CategoriesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;

        public CategoriesController(IMediator mediator, ApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // список суперкатегорий

            var categories = await _mediator.Send(new GetMainCategoriesQuery());
            return View(categories);
        }

        public async Task<IActionResult> Category(int id)
        {
            // список подкатегорий в суперкатегории

            var categories = await _mediator.Send(new GetSubcategoriesQuery(id));
            return View(categories);
        }
    }
}
