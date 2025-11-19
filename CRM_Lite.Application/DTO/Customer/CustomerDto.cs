namespace CRM_Lite.Application.DTO.Customer;

public record CustomerDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
}