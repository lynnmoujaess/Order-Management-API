using OrderManagement_Api.DTOs.Customers;
using OrderManagement_Api.Models;
using OrderManagement_Api.Repositories.Interfaces;
using OrderManagement_Api.Services.Interfaces;

namespace OrderManagement_Api.Services.Implementations;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public async Task<IEnumerable<CustomerResponseDto>> GetAllAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return customers.Select(MapToResponseDto);
    }

    public async Task<CustomerResponseDto?> GetByIdAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer is null) return null;
        return MapToResponseDto(customer);
    }

    public async Task<CustomerResponseDto> CreateAsync(CreateCustomerDto dto)
    {
        var customer = new Customer
        {
            Name = dto.Name,
            Email = dto.Email
        };

        await _customerRepository.AddAsync(customer);
        await _customerRepository.SaveChangesAsync();
        return MapToResponseDto(customer);
    }

    private static CustomerResponseDto MapToResponseDto(Customer customer) => new CustomerResponseDto
    {
        Id = customer.Id,
        Name = customer.Name,
        Email = customer.Email,
    };
}