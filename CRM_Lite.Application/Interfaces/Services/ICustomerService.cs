using CRM_Lite.Application.DTO;
using CRM_Lite.Application.DTO.Customer;
using CRM_Lite.Application.DTO.Pagination;

namespace CRM_Lite.Application.Interfaces.Services;

public interface ICustomerService
{
    Task<PagedResponse<CustomerDto>> GetCustomersByBirthday(DateTime birthday, int pageNumber, int pageSize);
    Task<CustomerDto> CreateCustomer(CreateCustomerDto dto);
    Task<PagedResponse<BuyerDto>> GetLastBuyers(int days, int pageNumber, int pageSize);
}