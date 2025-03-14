using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using Product.Infrastructure.Data;
using System.Text.Json;

namespace Product.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;
        private readonly IDistributedCache _cache;
        private readonly JsonSerializerOptions _jsonOptions;

        public ProductRepository(ProductDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;

            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<List<Products>> GetAllAsync()
        {
            string cacheKey = "products_all";
            string cachedData = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<List<Products>>(cachedData, _jsonOptions);
            }

            var products = await _context.Products.ToListAsync();

            if (products != null && products.Count > 0)
            {
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                };

                string jsonData = JsonSerializer.Serialize(products, _jsonOptions);
                await _cache.SetStringAsync(cacheKey, jsonData, options);
            }

            return products;
        }

        public async Task<Products> GetByIdAsync(int id)
        {
            string cacheKey = $"product_{id}";
            string cachedData = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<Products>(cachedData, _jsonOptions);
            }

            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                };

                string jsonData = JsonSerializer.Serialize(product, _jsonOptions);
                await _cache.SetStringAsync(cacheKey, jsonData, options);
            }

            return product;
        }

        public async Task AddAsync(Products product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            await InvalidateCache();
        }

        public async Task UpdateAsync(Products product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            await InvalidateCache();
        }

        public async Task DeleteAsync(Products product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            await InvalidateCache();
        }

        private async Task InvalidateCache()
        {
            await _cache.RemoveAsync("products_all");
            var products = await _context.Products.ToListAsync();
            foreach (var product in products)
            {
                await _cache.RemoveAsync($"product_{product.Id}");
            }
        }
    }
}
