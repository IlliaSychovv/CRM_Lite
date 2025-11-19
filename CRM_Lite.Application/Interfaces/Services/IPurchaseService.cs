using CRM_Lite.Application.DTO;
using CRM_Lite.Application.DTO.Pagination;
using CRM_Lite.Application.DTO.Purchase;

namespace CRM_Lite.Application.Interfaces.Services;

public interface IPurchaseService
{
    Task<PagedResponse<CategoryCountDto>> GetPurchasedCategoriesById(Guid customersId, int pageNumber, int pageSize);
    Task<Guid> CreateNewPurchaseAsync(CreatePurchaseDto dto);
}