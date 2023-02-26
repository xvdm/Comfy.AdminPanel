using MediatR;

namespace AdminPanel.Commands.Products
{
    public class DeleteProductCharacteristicCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteProductCharacteristicCommand(int id)
        {
            Id = id;
        }
    }
}
