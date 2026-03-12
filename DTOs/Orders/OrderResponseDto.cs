namespace OrderManagement_Api.DTOs.Orders;

public class OrderResponseDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public required string CustomerName { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemResponseDto> OrderItems { get; set; } = [];
    public decimal TotalAmount { get; set; }
}