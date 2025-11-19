using CRM_Lite.Application.DTO;
using CRM_Lite.Application.DTO.Purchase;
using CRM_Lite.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Lite.Controllers;

[ApiController]
[Route("api/v1/purchase")]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public PurchaseController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPurchasesAsync([FromQuery] Guid customerId,
        [FromQuery] int page = 1, [FromQuery]int pageSize = 10)
    {
        var result = await _purchaseService.GetPurchasedCategoriesById(customerId, page, pageSize);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddPurchaseAsync([FromBody] CreatePurchaseDto dto)
    {
        var purchaseId = await _purchaseService.CreateNewPurchaseAsync(dto);
        return Ok(purchaseId);
    }
}