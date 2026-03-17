using MassTransit;
using OrderManagement_Api.Events;

namespace OrderManagement_Api.Consumers;

public class OrderPlacedConsumer: IConsumer<OrderPlacedEvent>
{
    private readonly ILogger<OrderPlacedConsumer> _logger;

    public OrderPlacedConsumer(ILogger<OrderPlacedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OrderPlacedEvent> context)
    {
        var order = context.Message;

        _logger.LogInformation(
            "Order placed event received — OrderId: {OrderId}, CustomerId: {CustomerId}, Total: {TotalAmount}",
            order.OrderId,
            order.CustomerId,
            order.TotalAmount);

        foreach (var item in order.Items)
        {
            _logger.LogInformation(
                "  → Product: {ProductName}, Quantity: {Quantity}, UnitPrice: {UnitPrice}",
                item.ProductName,
                item.Quantity,
                item.UnitPrice);
        }

        return Task.CompletedTask;
    }
}