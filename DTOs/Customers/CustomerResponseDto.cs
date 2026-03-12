namespace OrderManagement_Api.DTOs.Customers;

public class CustomerResponseDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}