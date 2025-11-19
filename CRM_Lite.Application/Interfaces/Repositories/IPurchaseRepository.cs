using CRM_Lite.Application.DTO;
using CRM_Lite.Application.DTO.Pagination;
using CRM_Lite.Application.DTO.Purchase;
using CRM_Lite.Domain.Entity;

namespace CRM_Lite.Application.Interfaces.Repositories;

public interface IPurchaseRepository
{
    Task<PagedResult<CategoryCountDto>> GetPurchasedCategoriesByIdAsync(Guid customersId, int page, int pageSize);
    Task CreatePurchaseAsync(Purchase purchase); 
}