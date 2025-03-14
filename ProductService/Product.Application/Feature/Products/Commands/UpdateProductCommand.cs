using MediatR;
using Product.Domain.DTOs;

namespace Product.Application.Feature.Products.Commands
{
    public class UpdateProductCommand : IRequest<UpdateProductCommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
