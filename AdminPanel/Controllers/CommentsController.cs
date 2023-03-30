using AdminPanel.MediatorHandlers.Questions;
using AdminPanel.MediatorHandlers.Reviews;
using AdminPanel.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class CommentsController : Controller
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> ActiveComments(int? pageSize, int? pageNumber)
        {
            var questions = await _mediator.Send(new GetQuestionsQuery(true, pageSize, pageNumber));
            var reviews = await _mediator.Send(new GetReviewsQuery(true, pageSize, pageNumber));
            var viewModel = new CommentsViewModel()
            {
                Questions = questions,
                Reviews = reviews
            };
            return View(viewModel);
        }

        public async Task<IActionResult> InactiveComments(int? pageSize, int? pageNumber)
        {
            var questions = await _mediator.Send(new GetQuestionsQuery(false, pageSize, pageNumber));
            var reviews = await _mediator.Send(new GetReviewsQuery(false, pageSize, pageNumber));
            var viewModel = new CommentsViewModel()
            {
                Questions = questions,
                Reviews = reviews
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeQuestionActivityStatus(int id, bool isActive)
        {
            await _mediator.Send(new ChangeQuestionActivityStatusCommand(id, isActive));
            return Redirect("/Comments/InactiveComments");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeQuestionAnswerActivityStatus(int id, bool isActive)
        {
            await _mediator.Send(new ChangeQuestionAnswerActivityStatusCommand(id, isActive));
            return Redirect("/Comments/InactiveComments");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeReviewActivityStatus(int id, bool isActive)
        {
            await _mediator.Send(new ChangeReviewActivityStatusCommand(id, isActive));
            return Redirect("/Comments/InactiveComments");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeReviewAnswerActivityStatus(int id, bool isActive)
        {
            await _mediator.Send(new ChangeReviewAnswerActivityStatusCommand(id, isActive));
            return Redirect("/Comments/InactiveComments");
        }
    }
}
