using CRM_Lite.Application.DTO;
using CRM_Lite.Application.DTO.Customer;
using CRM_Lite.Application.DTO.Pagination;
using CRM_Lite.Application.Interfaces.Repositories;
using CRM_Lite.Application.Services;
using CRM_Lite.Domain.Entity;
using Moq;
using Shouldly;

namespace CRM_Lite.UnitTest.Tests;

public class CustomerServiceTest
{
    private readonly Mock<ICustomerRepository> _mockRepository;
    private readonly CustomerService _customerService;

    public CustomerServiceTest()
    {
        _mockRepository = new Mock<ICustomerRepository>();
        _customerService = new CustomerService(_mockRepository.Object);
    }

    [Fact]
    public async Task CreateCustomer_ShouldReturn_SuccessResult()
    {
        var createDto = new CreateCustomerDto
        {
            FullName = "John Doe",
            BirthDate = DateTime.UtcNow.AddYears(-30)
        };

        _mockRepository
            .Setup(r => r.CreateCustomerAsync(It.IsAny<Customer>()))
            .Returns(Task.CompletedTask);

        var result = await _customerService.CreateCustomer(createDto);

        result.FullName.ShouldBe(createDto.FullName);
        result.Id.ShouldNotBe(Guid.Empty);
        _mockRepository.Verify(r => r.CreateCustomerAsync(It.IsAny<Customer>()), Times.Once);
    }

    [Fact]
    public async Task GetCustomersByBirthday_ShouldReturn_SuccessResult()
    {
        var birthday = new DateTime(1990, 1, 1);
        var customers = new List<Customer>
        {
            new Customer
                { Id = Guid.NewGuid(), FullName = "Alice", BirthDate = birthday, RegistrationDate = DateTime.UtcNow },
            new Customer
                { Id = Guid.NewGuid(), FullName = "Bob", BirthDate = birthday, RegistrationDate = DateTime.UtcNow }
        };

        var pagedResult = new PagedResult<Customer>
        {
            Items = customers,
            TotalCount = customers.Count
        };

        _mockRepository
            .Setup(r => r.GetCustomersByBirthDayAsync(birthday, 1, 10))
            .ReturnsAsync(pagedResult);

        var result = await _customerService.GetCustomersByBirthday(birthday, 1, 10);

        result.TotalCount.ShouldBe(2);
        result.Items.Count.ShouldBe(2);
        result.Items.Select(x => x.FullName).ShouldContain("Alice");
        result.Items.Select(x => x.FullName).ShouldContain("Bob");
        _mockRepository.Verify(r => r.GetCustomersByBirthDayAsync(birthday, 1, 10), Times.Once);
    }

    [Fact]
    public async Task GetLastBuyers_ShouldReturn_SuccessResult()
    {
        var sampleData = new PagedResult<BuyerDto>
        {
            TotalCount = 2,
            Items = new List<BuyerDto>
            {
                new BuyerDto
                    { Id = Guid.NewGuid(), FullName = "test1", LastPurchaseDate = DateTime.UtcNow.AddDays(-1) },
                new BuyerDto { Id = Guid.NewGuid(), FullName = "test2", LastPurchaseDate = DateTime.UtcNow.AddDays(-2) }
            }
        };

        int days = 7;
        int pageNumber = 1;
        int pageSize = 10;

        _mockRepository.Setup(r => r.GetLastBuyersAsync(days, pageNumber, pageSize))
            .ReturnsAsync(sampleData);

        var result = await _customerService.GetLastBuyers(days, pageNumber, pageSize);

        result.TotalCount.ShouldBe(2);
        result.Items.Count.ShouldBe(2);
        result.Items.Select(x => x.FullName).ShouldContain("test1");
        result.Items.Select(x => x.FullName).ShouldContain("test2");
        _mockRepository.Verify(r => r.GetLastBuyersAsync(7, 1, 10), Times.Once);
    }
}
