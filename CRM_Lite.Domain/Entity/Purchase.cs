namespace CRM_Lite.Domain.Entity;

public class Purchase
{
    public Guid Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime PurchaseDate { get; set; }
    
    public Customer? Customer { get; set; }
    public List<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();
}