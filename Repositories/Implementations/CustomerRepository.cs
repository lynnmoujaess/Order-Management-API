using Microsoft.EntityFrameworkCore;
using OrderManagement_Api.Data;
using OrderManagement_Api.Models;
using OrderManagement_Api.Repositories.Interfaces;

namespace OrderManagement_Api.Repositories.Implementations;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _context.Customers.FindAsync(id);  
    }

    public async Task AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}