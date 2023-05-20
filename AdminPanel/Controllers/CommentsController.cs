using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Questions;
using AdminPanel.MediatorHandlers.Reviews;
using AdminPanel.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers;


[AutoValidateAntiforgeryToken]
[Authorize(Policy = RoleNames.Administrator)]
public sealed class CommentsController : Controller
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

        var totalCount = await _mediator.Send(new GetQuestionsTotalCountQuery(true));
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

        var totalCount = await _mediator.Send(new GetReviewsTotalCountQuery(true));
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

        var totalCount = await _mediator.Send(new GetQuestionsTotalCountQuery(false));
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

        var totalCount = await _mediator.Send(new GetReviewsTotalCountQuery(false));
        var totalPages = (totalCount - 1) / (query.PageSize + 1);

        var viewModel = new ReviewsViewModel()
        {
            Reviews = reviews,
            TotalPages = totalPages,
            CurrentPage = query.PageNumber
        };
        return View(viewModel);
    }

    public async Task<IActionResult> ActiveReviewAnswers(int? pageSize, int? pageNumber)
    {
        var query = new GetReviewAnswersQuery(true, pageSize, pageNumber);
        var reviewAnswers = await _mediator.Send(query);

        var totalCount = await _mediator.Send(new GetReviewAnswersTotalCountQuery(true));
        var totalPages = (totalCount - 1) / (query.PageSize + 1);

        var viewModel = new ReviewAnswersViewModel()
        {
            ReviewAnswers = reviewAnswers,
            TotalPages = totalPages,
            CurrentPage = query.PageNumber
        };
        return View(viewModel);
    }

    public async Task<IActionResult> ActiveQuestionAnswers(int? pageSize, int? pageNumber)
    {
        var query = new GetQuestionAnswersQuery(true, pageSize, pageNumber);
        var questionAnswers = await _mediator.Send(query);

        var totalCount = await _mediator.Send(new GetReviewAnswersTotalCountQuery(true));
        var totalPages = (totalCount - 1) / (query.PageSize + 1);

        var viewModel = new QuestionAnswersViewModel()
        {
            QuestionAnswers = questionAnswers,
            TotalPages = totalPages,
            CurrentPage = query.PageNumber
        };
        return View(viewModel);
    }

    public async Task<IActionResult> InactiveReviewAnswers(int? pageSize, int? pageNumber)
    {
        var query = new GetReviewAnswersQuery(false, pageSize, pageNumber);
        var reviewAnswers = await _mediator.Send(query);

        var totalCount = await _mediator.Send(new GetReviewAnswersTotalCountQuery(false));
        var totalPages = (totalCount - 1) / (query.PageSize + 1);

        var viewModel = new ReviewAnswersViewModel()
        {
            ReviewAnswers = reviewAnswers,
            TotalPages = totalPages,
            CurrentPage = query.PageNumber
        };
        return View(viewModel);
    }

    public async Task<IActionResult> InactiveQuestionAnswers(int? pageSize, int? pageNumber)
    {
        var query = new GetQuestionAnswersQuery(false, pageSize, pageNumber);
        var questionAnswers = await _mediator.Send(query);

        var totalCount = await _mediator.Send(new GetReviewAnswersTotalCountQuery(false));
        var totalPages = (totalCount - 1) / (query.PageSize + 1);

        var viewModel = new QuestionAnswersViewModel()
        {
            QuestionAnswers = questionAnswers,
            TotalPages = totalPages,
            CurrentPage = query.PageNumber
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ChangeQuestionActivityStatus(int id, bool isActive)
    {
        await _mediator.Send(new UpdateQuestionActivityStatusCommand(id, isActive));
        return RedirectToAction(isActive ? nameof(InactiveQuestions) : nameof(ActiveQuestions));
    }

    [HttpPost]
    public async Task<IActionResult> ChangeQuestionAnswerActivityStatus(int id, bool isActive)
    {
        await _mediator.Send(new UpdateQuestionAnswerActivityStatusCommand(id, isActive));
        return RedirectToAction(isActive ? nameof(InactiveQuestionAnswers) : nameof(ActiveQuestionAnswers));
    }

    [HttpPost]
    public async Task<IActionResult> ChangeReviewActivityStatus(int id, bool isActive)
    {
        await _mediator.Send(new UpdateReviewActivityStatusCommand(id, isActive));
        return RedirectToAction(isActive ? nameof(InactiveReviews) : nameof(ActiveReviews));
    }

    [HttpPost]
    public async Task<IActionResult> ChangeReviewAnswerActivityStatus(int id, bool isActive)
    {
        await _mediator.Send(new UpdateReviewAnswerActivityStatusCommand(id, isActive));
        return RedirectToAction(isActive ? nameof(InactiveReviewAnswers) : nameof(ActiveReviewAnswers));
    }
}