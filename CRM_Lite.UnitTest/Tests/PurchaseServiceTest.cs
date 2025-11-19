using CRM_Lite.Application.DTO.Pagination;
using CRM_Lite.Application.DTO.Purchase;
using CRM_Lite.Application.Interfaces.Repositories;
using CRM_Lite.Application.Services;
using CRM_Lite.Domain.Entity;
using Moq;
using Shouldly;

namespace CRM_Lite.UnitTest.Tests;

public class PurchaseServiceTest
{
    private readonly Mock<IPurchaseRepository> _purchaseRepositoryMock;
    private readonly PurchaseService _purchaseService;

    public PurchaseServiceTest()
    {
        _purchaseRepositoryMock = new Mock<IPurchaseRepository>();
        _purchaseService = new PurchaseService( _purchaseRepositoryMock.Object);
    }
    
    [Fact]
    public async Task GetPurchasedCategoriesById_ShouldReturnPagedResponse()
    {
        var customerId = Guid.NewGuid();
        int pageNumber = 1;
        int pageSize = 10;

        var pagedResult = new PagedResult<CategoryCountDto>
        {
            TotalCount = 2,
            Items = new List<CategoryCountDto>
            {
                new CategoryCountDto { Category = ProductCategory.Electronics, Quantity = 5 },
                new CategoryCountDto { Category = ProductCategory.Food, Quantity = 3 }
            }
        };

        _purchaseRepositoryMock
            .Setup(r => r.GetPurchasedCategoriesByIdAsync(customerId, pageNumber, pageSize))
            .ReturnsAsync(pagedResult);

        var result = await _purchaseService.GetPurchasedCategoriesById(customerId, pageNumber, pageSize);

        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(2);
        result.PageSize.ShouldBe(pageSize);
        result.CurrentPage.ShouldBe(pageNumber);
        result.Items.Count.ShouldBe(2);
        result.Items.ShouldContain(x => x.Category == ProductCategory.Electronics && x.Quantity == 5);
        result.Items.ShouldContain(x => x.Category == ProductCategory.Food && x.Quantity == 3);
        _purchaseRepositoryMock.Verify(r => r.GetPurchasedCategoriesByIdAsync(customerId, pageNumber, pageSize), Times.Once);
    }
    
    [Fact]
    public async Task CreateNewPurchaseAsync_ShouldCallRepositoryAndReturnPurchaseId()
    {
        var customerId = Guid.NewGuid();
        var dto = new CreatePurchaseDto
        {
            CustomerId = customerId,
            Price = 500,
            Items = new List<PurchaseItemDto>
            {
                new PurchaseItemDto { ProductId = Guid.NewGuid(), Quantity = 2 },
                new PurchaseItemDto { ProductId = Guid.NewGuid(), Quantity = 1 }
            }
        };

        Purchase? savedPurchase = null;

        _purchaseRepositoryMock
            .Setup(r => r.CreatePurchaseAsync(It.IsAny<Purchase>()))
            .Returns(Task.CompletedTask)
            .Callback<Purchase>(p => savedPurchase = p);

        var result = await _purchaseService.CreateNewPurchaseAsync(dto);

        result.ShouldNotBe(Guid.Empty);

        savedPurchase.ShouldNotBeNull();
        savedPurchase!.CustomerId.ShouldBe(customerId);
        savedPurchase.TotalPrice.ShouldBe(dto.Price);
        savedPurchase.PurchaseItems.Count.ShouldBe(dto.Items.Count);
        savedPurchase.PurchaseItems[0].ProductId.ShouldBe(dto.Items[0].ProductId);
        savedPurchase.PurchaseItems[0].Quantity.ShouldBe(dto.Items[0].Quantity);
        _purchaseRepositoryMock.Verify(r => r.CreatePurchaseAsync(It.IsAny<Purchase>()), Times.Once);
    }

}