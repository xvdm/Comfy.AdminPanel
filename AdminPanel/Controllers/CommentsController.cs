using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Questions;
using AdminPanel.MediatorHandlers.Reviews;
using AdminPanel.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Policy = PoliciesNames.Administrator)]
    public class CommentsController : Controller
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> ActiveQuestions(int? pageSize, int? pageNumber)
        {
            var query = new GetQuestionsQuery(true, pageSize, pageNumber);
            var questions = await _mediator.Send(query);

            var totalCount = await _mediator.Send(new GetQuestionsTotalCountQuery());
            var totalPages = (totalCount - 1) / query.PageSize + 1;

            var viewModel = new QuestionsViewModel()
            {
                Questions = questions,
                TotalPages = totalPages,
                CurrentPage = query.PageNumber
            };
            return View(viewModel);
        }

        public async Task<IActionResult> ActiveReviews(int? pageSize, int? pageNumber)
        {
            var query = new GetReviewsQuery(true, pageSize, pageNumber);
            var reviews = await _mediator.Send(query);

            var totalCount = await _mediator.Send(new GetReviewsTotalCountQuery());
            var totalPages = (totalCount - 1) / query.PageSize + 1;

            var viewModel = new ReviewsViewModel()
            {
                Reviews = reviews,
                TotalPages = totalPages,
                CurrentPage = query.PageNumber
            };
            return View(viewModel);
        }

        public async Task<IActionResult> InactiveQuestions(int? pageSize, int? pageNumber)
        {
            var query = new GetQuestionsQuery(false, pageSize, pageNumber);
            var questions = await _mediator.Send(query);

            var totalCount = await _mediator.Send(new GetQuestionsTotalCountQuery());
            var totalPages = (totalCount - 1) / (query.PageSize + 1);

            var viewModel = new QuestionsViewModel()
            {
                Questions = questions,
                TotalPages = totalPages,
                CurrentPage = query.PageNumber
            };
            return View(viewModel);
        }

        public async Task<IActionResult> InactiveReviews(int? pageSize, int? pageNumber)
        {
            var query = new GetReviewsQuery(false, pageSize, pageNumber);
            var reviews = await _mediator.Send(query);

            var totalCount = await _mediator.Send(new GetReviewsTotalCountQuery());
            var totalPages = (totalCount - 1) / (query.PageSize + 1);

            var viewModel = new ReviewsViewModel()
            {
                Reviews = reviews,
                TotalPages = totalPages,
                CurrentPage = query.PageNumber
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeQuestionActivityStatus(int id, bool isActive)
        {
            await _mediator.Send(new ChangeQuestionActivityStatusCommand(id, isActive));
            return Redirect(isActive ? "/Comments/InactiveQuestions" : "/Comments/ActiveQuestions");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeQuestionAnswerActivityStatus(int id, bool isActive)
        {
            await _mediator.Send(new ChangeQuestionAnswerActivityStatusCommand(id, isActive));
            return Redirect(isActive ? "/Comments/InactiveQuestions" : "/Comments/ActiveQuestions");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeReviewActivityStatus(int id, bool isActive)
        {
            await _mediator.Send(new ChangeReviewActivityStatusCommand(id, isActive));
            return Redirect(isActive ? "/Comments/InactiveReviews" : "/Comments/ActiveReviews");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeReviewAnswerActivityStatus(int id, bool isActive)
        {
            await _mediator.Send(new ChangeReviewAnswerActivityStatusCommand(id, isActive));
            return Redirect(isActive ? "/Comments/InactiveReviews" : "/Comments/ActiveReviews");
        }
    }
}
