﻿using AdminPanel.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.MediatorHandlers.Questions
{
    public class GetQuestionsTotalCountQuery : IRequest<int>
    {
    }

    public class GetQuestionsTotalCountQueryHandler : IRequestHandler<GetQuestionsTotalCountQuery, int>
    {
        private readonly ApplicationDbContext _context;

        public GetQuestionsTotalCountQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(GetQuestionsTotalCountQuery request, CancellationToken cancellationToken)
        {
            return await _context.Questions.CountAsync(cancellationToken);
        }
    }
}