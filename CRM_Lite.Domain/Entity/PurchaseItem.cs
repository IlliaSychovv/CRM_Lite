namespace CRM_Lite.Domain.Entity;

public class PurchaseItem
{
    public Guid Id { get; set; }
    public Guid PurchaseId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    
    public Product? Product { get; set; } 
    public Purchase? Purchase { get; set; } 
}