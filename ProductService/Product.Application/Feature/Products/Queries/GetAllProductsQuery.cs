using MediatR;
using Product.Domain.DTOs;

namespace Product.Application.CQRS.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>> { }
}
