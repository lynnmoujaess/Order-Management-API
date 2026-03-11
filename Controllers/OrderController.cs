// using Microsoft.AspNetCore.Mvc;
// using OrderManagement_Api.Models;
// using OrderManagement_Api.Services;
//
// namespace OrderManagement_Api.Controllers;
//
// [ApiController]
// [Route("api/orders")]
// public class OrderController : ControllerBase
// {
//     private readonly OrderService _service;
//
//     public OrderController(OrderService service)
//     {
//         _service = service;
//     }
//
//     [HttpGet]
//     public IActionResult GetAll()
//     {
//         return Ok(_service.GetAll());
//     }
//
//     [HttpPost]
//     public IActionResult Create(Order order)
//     {
//         return Ok(_service.Create(order));
//     }
//
//     [HttpPut("{id}")]
//     public IActionResult Update(Guid id, Order order)
//     {
//         var result = _service.Update(id, order);
//
//         if (result == null)
//             return NotFound();
//
//         return Ok(result);
//     }
// }