using OrderManagement_Api.Models;

namespace OrderManagement_Api.Repositories.Interfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int id);
    Task<Boolean> ExistsAsync(int id);
    Task AddAsync(Customer customer);
    Task SaveChangesAsync();
}