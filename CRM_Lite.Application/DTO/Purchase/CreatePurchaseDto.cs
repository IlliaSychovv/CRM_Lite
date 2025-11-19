namespace CRM_Lite.Application.DTO.Purchase;

public record CreatePurchaseDto
{
    public Guid CustomerId { get; set; }
    public decimal Price { get; set; }
    public List<PurchaseItemDto> Items { get; set; } = new List<PurchaseItemDto>();
}

public record PurchaseItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}