using MassTransit;
using OrderManagement_Api.Common.Exceptions;
using OrderManagement_Api.DTOs.Orders;
using OrderManagement_Api.Events;
using OrderManagement_Api.Models;
using OrderManagement_Api.Repositories.Interfaces;
using OrderManagement_Api.Services.Interfaces;

namespace OrderManagement_Api.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IPublishEndpoint _publishEndpoint;


    public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository, IProductRepository productRepository, IPublishEndpoint publishEndpoint)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _publishEndpoint = publishEndpoint;
    }
    
    public async Task<IEnumerable<OrderResponseDto>> GetAllAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return orders.Select(MapToResponseDto);
        
    }

    public async Task<OrderResponseDto?> GetByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order is null) return null;
        return MapToResponseDto(order);
    }

    public async Task<OrderResponseDto> CreateAsync(CreateOrderDto dto)
    {
        var customerExists = await _customerRepository.ExistsAsync(dto.CustomerId);
        if (!customerExists) throw new NotFoundException($"Customer with id {dto.CustomerId} not found");
        
        var productIds = dto.OrderItems.Select(i => i.ProductId).ToList();
        var products = await _productRepository.GetByIdsAsync(productIds);
        
        var orderItems = new List<OrderItem>();

        foreach (var itemDto in dto.OrderItems)
        {
            var product = products.FirstOrDefault(p => p.Id == itemDto.ProductId);
            
            if (product is null)
                throw new NotFoundException($"Product with id {itemDto.ProductId} not found");

            if (product.Stock < itemDto.Quantity)
                throw new BadRequestException($"Insufficient stock for product '{product.Name}'. Available: {product.Stock}, Requested: {itemDto.Quantity}");

            // Reduce stock
            product.Stock -= itemDto.Quantity;
            
            orderItems.Add(new OrderItem
            {
                ProductId = product.Id,
                Quantity = itemDto.Quantity,
                UnitPrice = product.Price
            });
        }
        
        var order = new Order
        {
            CustomerId = dto.CustomerId,
            OrderDate = DateTime.UtcNow,
            OrderItems = orderItems
        };
        
        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveChangesAsync();

        var createdOrder = await _orderRepository.GetByIdAsync(order.Id);
        
        await _publishEndpoint.Publish(new OrderPlacedEvent
        {
            OrderId = createdOrder!.Id,
            CustomerId = createdOrder.CustomerId,
            OrderDate = createdOrder.OrderDate,
            TotalAmount = createdOrder.OrderItems.Sum(i => i.UnitPrice * i.Quantity),
            Items = createdOrder.OrderItems.Select(i => new OrderPlacedEventItem
            {
                ProductId = i.ProductId,
                ProductName = i.Product.Name,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        });
        
        return MapToResponseDto(createdOrder!);
    }

    private static OrderResponseDto MapToResponseDto(Order order)
    {
        return new OrderResponseDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            CustomerName = order.Customer!.Name,
            OrderDate = order.OrderDate,
            OrderItems = order.OrderItems.Select(i => new OrderItemResponseDto
            {
                Id = i.Id,
                ProductId = i.ProductId,
                ProductName = i.Product!.Name,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList(),
            TotalAmount = order.OrderItems.Sum(i => i.UnitPrice * i.Quantity)
        };
    }
}