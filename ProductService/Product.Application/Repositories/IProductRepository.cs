
using Product.Domain.Entities;

namespace Product.Application.Interfaces

{
    public interface IProductRepository
    {
        Task<List<Products>> GetAllAsync();
        Task<Products> GetByIdAsync(int id);
        Task AddAsync(Products product);
        Task UpdateAsync(Products product);
        Task DeleteAsync(Products product);
    }
}
