using CRM_Lite.Application.DTO;
using CRM_Lite.Application.DTO.Pagination;
using CRM_Lite.Application.Intarfaces;
using CRM_Lite.Domain.Entity;
using SequentialGuid;

namespace CRM_Lite.Application.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IPurchaseRepository _repository;

    public PurchaseService(IPurchaseRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResponse<CategoryCountDto>> GetPurchasedCategoriesById(Guid customersId, int pageNumber, int pageSize)
    {
        var pagedResult = await _repository.GetPurchasedCategoriesByIdAsync(customersId, pageNumber, pageSize);

        return new PagedResponse<CategoryCountDto>
        {
            Items = pagedResult.Items,
            TotalCount = pagedResult.TotalCount,
            CurrentPage = pageNumber,
            PageSize = pageSize
        };
    }
    
    public async Task<Guid> CreateNewPurchaseAsync(CreatePurchaseDto dto)
    {
        var newPurchase = new Purchase
        {
            Id = SequentialGuidGenerator.Instance.NewGuid(),
            CustomerId = dto.CustomerId,
            PurchaseDate = DateTime.UtcNow,
            Number = $"P-{DateTime.UtcNow.Ticks}", 
            TotalPrice = dto.Price,
            PurchaseItems = dto.Items.Select(itemDto => new PurchaseItem
            {
                Id = SequentialGuidGenerator.Instance.NewGuid(),
                ProductId = itemDto.ProductId,
                Quantity = itemDto.Quantity
            }).ToList()
        };

        await _repository.CreatePurchaseAsync(newPurchase);

        return newPurchase.Id;
    }
}