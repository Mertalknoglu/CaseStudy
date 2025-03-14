using MediatR;
using Product.Domain.DTOs;

namespace Product.Application.Feature.Products.Commands
{
    public class CreateProductCommand : IRequest<CreateProductCommandResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
