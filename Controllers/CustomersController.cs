using Microsoft.AspNetCore.Mvc;
using OrderManagement_Api.DTOs.Customers;
using OrderManagement_Api.Services.Interfaces;

namespace OrderManagement_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(ICustomerService customerService, ILogger<CustomersController> logger)
    {
        _customerService = customerService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _customerService.GetAllAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _customerService.GetByIdAsync(id);
        if (customer is null)
        {
            _logger.LogWarning("Customer with id {id} not found", id);
            return NotFound();
        }
        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
    {
        var customer = await _customerService.CreateAsync(dto);
        _logger.LogInformation("Customer with id {CustomerId} created", customer.Id);
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
    }
}