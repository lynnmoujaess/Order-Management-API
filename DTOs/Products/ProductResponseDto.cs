namespace OrderManagement_Api.DTOs.Products;

public class ProductResponseDto
{
    public int  Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}