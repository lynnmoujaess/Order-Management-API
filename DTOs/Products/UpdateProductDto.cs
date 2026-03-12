namespace OrderManagement_Api.DTOs.Products;

public class UpdateProductDto
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}