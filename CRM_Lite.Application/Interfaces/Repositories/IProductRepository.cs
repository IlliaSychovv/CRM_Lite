using CRM_Lite.Domain.Entity;

namespace CRM_Lite.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task CreateProduct(Product product);
}