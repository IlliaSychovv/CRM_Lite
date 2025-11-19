using CRM_Lite.Domain.Entity;

namespace CRM_Lite.Application.Intarfaces;

public interface IProductRepository
{
    Task CreateProduct(Product product);
}