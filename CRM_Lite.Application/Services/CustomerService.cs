using CRM_Lite.Application.DTO;
using CRM_Lite.Application.DTO.Customer;
using CRM_Lite.Application.DTO.Pagination;
using CRM_Lite.Application.Interfaces.Repositories;
using CRM_Lite.Application.Interfaces.Services;
using CRM_Lite.Domain.Entity;
using SequentialGuid;
using Mapster;

namespace CRM_Lite.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<PagedResponse<CustomerDto>> GetCustomersByBirthday(DateTime birthday, int pageNumber, int pageSize)
    {
        var pagedResult = await _customerRepository.GetCustomersByBirthDayAsync(birthday, pageNumber, pageSize);

        return new PagedResponse<CustomerDto>
        {
            Items = pagedResult.Items.Select(a => a.Adapt<CustomerDto>()).ToList(),
            TotalCount = pagedResult.TotalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
    }

    public async Task<PagedResponse<BuyerDto>> GetLastBuyers(int days, int pageNumber, int pageSize)
    {
        var pagedResult = await _customerRepository.GetLastBuyersAsync(days, pageNumber, pageSize);

        return new PagedResponse<BuyerDto>
        {
            Items = pagedResult.Items.Select(a => a.Adapt<BuyerDto>()).ToList(),
            TotalCount = pagedResult.TotalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
    }

    public async Task<CustomerDto> CreateCustomer(CreateCustomerDto dto)
    {
        var customer = dto.Adapt<Customer>();
        customer.Id = SequentialGuidGenerator.Instance.NewGuid(); 
        customer.RegistrationDate = DateTime.UtcNow;
        
        await _customerRepository.CreateCustomerAsync(customer);
        
        return customer.Adapt<CustomerDto>();
    }
}