using CRM_Lite.Domain.Entity;

namespace CRM_Lite.Application.DTO;

public record CategoryCountDto
{
    public ProductCategory Category { get; set; }
    public int Quantity { get; set; }
}