namespace CRM_Lite.Application.DTO.Customer;

public record CreateCustomerDto
{
    public string FullName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}