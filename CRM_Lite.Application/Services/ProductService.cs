using CRM_Lite.Application.DTO;
using CRM_Lite.Application.Intarfaces;
using CRM_Lite.Domain.Entity;
using SequentialGuid;
using Mapster;

namespace CRM_Lite.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> CreateProduct(CreateProductDto dto)
    {
        var product = dto.Adapt<Product>();
        product.Id = SequentialGuidGenerator.Instance.NewGuid(); 
        
        await _productRepository.CreateProduct(product);
        
        return dto.Adapt<ProductDto>();
    }
}