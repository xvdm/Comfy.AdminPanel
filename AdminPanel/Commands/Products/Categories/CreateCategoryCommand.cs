using MediatR;
using WebApplication2.Models;

namespace AdminPanel.Commands.Products.Categories
{
    public class CreateCategoryCommand : IRequest
    {
        public Category Category { get; set; } = null!;

        public CreateCategoryCommand(Category category)
        {
            Category = category;
        }
    }
}
