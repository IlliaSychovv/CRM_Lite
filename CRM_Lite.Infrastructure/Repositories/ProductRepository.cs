using CRM_Lite.Application.Intarfaces;
using CRM_Lite.Domain.Entity;
using CRM_Lite.Infrastructure.Data;

namespace CRM_Lite.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateProduct(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }
}