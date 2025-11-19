using CRM_Lite.Application.DTO.Product;

namespace CRM_Lite.Application.Interfaces.Services;

public interface IProductService
{
    Task<ProductDto> CreateProduct(CreateProductDto dto);
}