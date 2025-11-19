namespace CRM_Lite.Domain.Entity;

public class Customer
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public DateTime RegistrationDate { get; set; }
    
    public List<Purchase> Purchases { get; set; } = new List<Purchase>();
}