namespace OrderManagement_Api.DTOs.Orders;

public class OrderItemResponseDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public required string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}