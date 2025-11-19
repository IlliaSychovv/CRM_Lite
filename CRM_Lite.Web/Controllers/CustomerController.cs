using CRM_Lite.Application.DTO.Customer;
using CRM_Lite.Application.Intarfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Lite.Controllers;

[ApiController]
[Route("api/v1/customer")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomersByBirthday([FromQuery] DateTime birthday, 
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var customers = await _customerService.GetCustomersByBirthday(birthday, page, pageSize);
        
        return Ok(customers);
    }

    [HttpGet("nigga")]
    public async Task<IActionResult> GetLastBuyers([FromQuery] int days,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var buyers = await _customerService.GetLastBuyers(days, page, pageSize);
        
        return Ok(buyers);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto dto)
    {
        var customer = await _customerService.CreateCustomer(dto);
        
        return Ok(customer);
    }
}