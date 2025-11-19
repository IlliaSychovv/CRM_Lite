namespace CRM_Lite.Application.DTO;

public class PurchaseItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CreatePurchaseDto
{
    public Guid CustomerId { get; set; }
    public decimal Price { get; set; }
    public List<PurchaseItemDto> Items { get; set; } = new List<PurchaseItemDto>();
}