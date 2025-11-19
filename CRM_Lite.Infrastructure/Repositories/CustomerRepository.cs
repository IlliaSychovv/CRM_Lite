using CRM_Lite.Application.DTO;
using CRM_Lite.Application.DTO.Pagination;
using CRM_Lite.Application.Interfaces.Repositories;
using CRM_Lite.Domain.Entity;
using CRM_Lite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM_Lite.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _dbContext;

    public CustomerRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<Customer>> GetCustomersByBirthDayAsync(DateTime birthday, int pageNumber, int pageSize)
    {
        var query = _dbContext.Customers
            .AsNoTracking()
            .AsQueryable();
        
        var count = await query.CountAsync();
        
        var items = await query
            .Where(x => x.BirthDate.Day == birthday.Day && x.BirthDate.Month == birthday.Month)
            .OrderByDescending(x => x.BirthDate)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Customer>
        {
            Items = items,
            TotalCount = count
        };
    }

    public async Task<PagedResult<BuyerDto>> GetLastBuyersAsync(int days, int pageNumber, int pageSize)
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-days);

        var filteredQuery = _dbContext.Purchases
            .AsNoTracking()
            .Where(x => x.PurchaseDate >= cutoffDate)
            .AsQueryable();

        var count = await filteredQuery
            .GroupBy(x => new { x.CustomerId, x.Customer.FullName })
            .CountAsync();

        var items = await filteredQuery
            .GroupBy(x => new { x.CustomerId, x.Customer.FullName })
            .Select(b => new BuyerDto
            {
                Id = b.Key.CustomerId,
                FullName = b.Key.FullName,
                LastPurchaseDate = b.Max(x => x.PurchaseDate)
            })
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<BuyerDto>
        {
            Items = items,
            TotalCount = count
        };
    }

    public async Task CreateCustomerAsync(Customer customer)
    {
        await _dbContext.Customers.AddAsync(customer);
        await _dbContext.SaveChangesAsync();
    }
}