using CRM_Lite.Application.DTO;

namespace CRM_Lite.Application.Services;

public interface IProductService
{
    Task<ProductDto> CreateProduct(CreateProductDto dto);
}