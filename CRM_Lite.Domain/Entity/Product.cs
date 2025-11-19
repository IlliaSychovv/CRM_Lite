namespace CRM_Lite.Domain.Entity;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ProductCategory Category  { get; set; }
    public string Article { get; set; } = string.Empty;
    public decimal Price { get; set; }
    
    public List<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();
}

public enum ProductCategory
{
    Electronics,
    Clothes,
    Food,
    Cars
} 