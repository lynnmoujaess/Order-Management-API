namespace OrderManagement_Api.DTOs.Orders;

public class CreateOrderDto
{
    public int CustomerId { get; set; }
    public List<CreateOrderItemDto> OrderItems { get; set; } = [];
}