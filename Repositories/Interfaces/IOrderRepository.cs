using OrderManagement_Api.Models;

namespace OrderManagement_Api.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(int id);
    Task AddAsync(Order order);
    Task SaveChangesAsync();
}