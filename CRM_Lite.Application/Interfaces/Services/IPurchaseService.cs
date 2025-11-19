using CRM_Lite.Application.DTO;
using CRM_Lite.Application.DTO.Pagination;

namespace CRM_Lite.Application.Intarfaces;

public interface IPurchaseService
{
    Task<PagedResponse<CategoryCountDto>> GetPurchasedCategoriesById(Guid customersId, int pageNumber, int pageSize);
    Task<Guid> CreateNewPurchaseAsync(CreatePurchaseDto dto);
}