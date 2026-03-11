// using OrderManagement_Api.Models;
//
// namespace OrderManagement_Api.Services;
//
// public class OrderService
// {
//     private readonly List<Order> _orders = [];
//     
//     public IEnumerable<Order> GetAll()
//     {
//         return _orders;
//     }
//
//
//     public Order Create(Order order)
//     {
//         order.Id = Guid.NewGuid();
//         order.CreatedAt = DateTime.UtcNow;
//         _orders.Add(order);
//         return order;
//     }
//
//     public Order? Update(Guid id, Order updated)
//     {
//         var existing = _orders.FirstOrDefault(o => o.Id == id);
//         if (existing == null)
//             return null;
//         existing.CustomerName = updated.CustomerName;
//         existing.Amount = updated.Amount;
//         return existing;
//     }
// }