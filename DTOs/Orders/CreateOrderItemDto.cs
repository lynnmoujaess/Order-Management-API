namespace OrderManagement_Api.DTOs.Orders;

public class CreateOrderItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}