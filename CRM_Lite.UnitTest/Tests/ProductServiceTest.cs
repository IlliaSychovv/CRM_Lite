using CRM_Lite.Application.DTO.Product;
using CRM_Lite.Application.Interfaces.Repositories;
using CRM_Lite.Application.Services;
using CRM_Lite.Domain.Entity;
using Moq;
using Shouldly;

namespace CRM_Lite.UnitTest.Tests;

public class ProductServiceTest
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly ProductService _productService;

    public ProductServiceTest()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _productService = new ProductService(_productRepositoryMock.Object);
    }
    
    [Fact]
    public async Task CreateProduct_ShouldCallRepositoryAndReturnProductDto()
    {
        var createDto = new CreateProductDto
        {
            Name = "Test Product",
            Category = ProductCategory.Electronics,
            Article = "TP-123",
            Price = 1000m
        };

        Product? savedProduct = null;

        _productRepositoryMock
            .Setup(r => r.CreateProduct(It.IsAny<Domain.Entity.Product>()))
            .Returns(Task.CompletedTask)
            .Callback<Domain.Entity.Product>(p => savedProduct = p);

        var result = await _productService.CreateProduct(createDto);
        
        savedProduct.Article.ShouldBe(createDto.Article);
        savedProduct.Price.ShouldBe(createDto.Price);
        savedProduct.Category.ShouldBe(createDto.Category);
        savedProduct.Id.ShouldNotBe(Guid.Empty);

        result.ShouldNotBeNull();
        result.Category.ShouldBe(createDto.Category);
        _productRepositoryMock.Verify(r => r.CreateProduct(It.IsAny<Domain.Entity.Product>()), Times.Once);
    }
}