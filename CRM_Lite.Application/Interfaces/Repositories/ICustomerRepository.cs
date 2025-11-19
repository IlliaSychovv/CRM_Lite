using CRM_Lite.Application.DTO;
using CRM_Lite.Application.DTO.Pagination;
using CRM_Lite.Domain.Entity;

namespace CRM_Lite.Application.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<PagedResult<Customer>> GetCustomersByBirthDayAsync(DateTime birthday, int pageNumber, int pageSize);
    Task CreateCustomerAsync(Customer customer);
    Task<PagedResult<BuyerDto>> GetLastBuyersAsync(int days, int pageNumber, int pageSize);
}