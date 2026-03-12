using OrderManagement_Api.DTOs.Orders;
using OrderManagement_Api.Models;
using OrderManagement_Api.Repositories.Interfaces;
using OrderManagement_Api.Services.Interfaces;

namespace OrderManagement_Api.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository,
        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<OrderResponseDto>> GetAllAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return orders.Select(MapToResponseDto);
    }

    public Task<OrderResponseDto?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<OrderResponseDto> CreateAsync(CreateOrderDto dto)
    {
        throw new NotImplementedException();
    }

    private static OrderResponseDto MapToResponseDto(Order order) => new OrderResponseDto
    {
        Id = order.Id,
        CustomerId = order.CustomerId,
        CustomerName = order.Customer.Name,
        OrderDate = order.OrderDate,
        OrderItems = order.OrderItems.Select(i => new OrderItemResponseDto
        {
            Id = i.Id,
            ProductId = i.ProductId,
            ProductName = i.Product.Name,
            Quantity = i.Quantity,
            UnitPrice = i.UnitPrice
        }).ToList(),
        TotalAmount = order.OrderItems.Sum(i => i.UnitPrice * i.Quantity)
    };
}