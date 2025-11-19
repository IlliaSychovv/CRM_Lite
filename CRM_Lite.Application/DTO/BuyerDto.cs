namespace CRM_Lite.Application.DTO;

public record BuyerDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime LastPurchaseDate { get; set; }
}