using CRM_Lite.Domain.Entity;

namespace CRM_Lite.Application.DTO;

public record ProductDto
{
    public string Name { get; set; } = string.Empty;
    public ProductCategory Category  { get; set; }
    public string Article { get; set; } = string.Empty;
    public decimal Price { get; set; }
}