using System.Collections.Generic;
using System.Threading.Tasks;
using ConcurrentOrdering.Domain.Models;

namespace ConcurrentOrdering.Domain.Infrastructure
{
    public interface IProductRepository
    {
        Task<List<Product>> FindAllAsync();
        Task<Product> FindByIdAsync(int id);
        Task<Product> UpdateAsync(Product product);
    }
}