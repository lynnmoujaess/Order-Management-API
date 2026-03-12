namespace OrderManagement_Api.DTOs.Orders;

public class CreateOrderDto
{
    public int CustomerId { get; set; }
    public List<CreateOrderItemDto> Items { get; set; } = [];
}