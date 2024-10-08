using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e_store.Data;
using e_store.Dto;
using e_store.Models;

namespace e_store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDtoesController : ControllerBase
    {
        private readonly EStoreDbContext _context;

        public OrderDtoesController(EStoreDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderDtoes
        [HttpGet]
        public IQueryable<OrderDto> GetAll()
        {
            var orders = from o in _context.Orders
                         select new OrderDto()
                         {
                             OrderID = o.OrderID,
                             OrderDate = o.OrderDate,
                             CustomerID=o.CustomerID
                            
                         };
            return orders;
        }

        // GET: api/OrderDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderDto(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderDto = new OrderDto
            {
                OrderID = order.OrderID,
                OrderDate = order.OrderDate,
                CustomerID=order.CustomerID
              
            };

            return orderDto;
        }

        // PUT: api/OrderDtoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderDto dto)
        {
            if (id != dto.OrderID)
            {
                return BadRequest();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            order.OrderID = dto.OrderID;
            order.OrderDate = dto.OrderDate;
            order.CustomerID = dto.CustomerID;
           

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!OrderDtoExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/OrderDtoes
        [HttpPost]
        public async Task<IActionResult> PostOrderDto(OrderDto orderDto)
        {
            var order = new Order
            {
                OrderID = orderDto.OrderID,
                OrderDate = orderDto.OrderDate,
                CustomerID=orderDto.CustomerID,
              
            };

            await _context.AddAsync(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        // DELETE: api/OrderDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDto(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderDtoExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrderID == id)).GetValueOrDefault();
        }
    }
}
