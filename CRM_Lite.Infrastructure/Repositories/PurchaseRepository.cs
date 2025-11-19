using CRM_Lite.Application.DTO;
using CRM_Lite.Application.DTO.Pagination;
using CRM_Lite.Application.DTO.Purchase;
using CRM_Lite.Application.Interfaces.Repositories;
using CRM_Lite.Domain.Entity;
using CRM_Lite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM_Lite.Infrastructure.Repositories;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly AppDbContext _dbContext;

    public PurchaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<CategoryCountDto>> GetPurchasedCategoriesByIdAsync(Guid customersId, int page, int pageSize)
    {
        var query = _dbContext.PurchaseItems
            .AsNoTracking()
            .Where(pi => pi.Purchase != null && pi.Purchase.CustomerId == customersId)
            .Select(pi => new 
            {
                Category = pi.Product!.Category,   
                pi.Quantity
            });

        var grouped = query.GroupBy(x => x.Category);

        var totalCount = await grouped.CountAsync();

        var items = await grouped
            .Select(g => new CategoryCountDto
            {
                Category = g.Key,  
                Quantity = g.Sum(x => x.Quantity)
            })
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<CategoryCountDto>
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task CreatePurchaseAsync(Purchase purchase)
    {
        await _dbContext.Purchases.AddAsync(purchase);
        await _dbContext.SaveChangesAsync();
    }
}