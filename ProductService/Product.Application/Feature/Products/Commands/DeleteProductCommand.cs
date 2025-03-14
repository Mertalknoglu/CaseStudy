using MediatR;
using Product.Domain.DTOs;

namespace Product.Application.Feature.Products.Commands
{
    public class DeleteProductCommand : IRequest<DeleteProductCommandResponse>
    {
        public int Id { get; set; }
    }
}
