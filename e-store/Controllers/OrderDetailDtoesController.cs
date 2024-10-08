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
    public class OrderDetailDtoesController : ControllerBase
    {
        private readonly EStoreDbContext _context;

        public OrderDetailDtoesController(EStoreDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetailDtoes
        [HttpGet]
        public IQueryable<OrderDetailDto> GetAll()
        {
            var orderDetails = from od in _context.OrderDetails
                               select new OrderDetailDto()
                               {
                                   OrderDetailID = od.OrderDetailID,
                                   OrderID = od.OrderID,
                                   ProductID = od.ProductID,
                                   Quantity = od.Quantity,
                                   UnitPrice = od.UnitPrice
                               };
            return orderDetails;
        }

        // GET: api/OrderDetailDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailDto>> GetOrderDetailDto(int id)
        {
            if (_context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails.FindAsync(id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            var orderDetailDto = new OrderDetailDto
            {
                OrderDetailID = orderDetail.OrderDetailID,
                OrderID = orderDetail.OrderID,
                ProductID = orderDetail.ProductID,
                Quantity = orderDetail.Quantity,
                UnitPrice = orderDetail.UnitPrice
            };

            return orderDetailDto;
        }

        // PUT: api/OrderDetailDtoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderDetail(int id, OrderDetailDto dto)
        {
            if (id != dto.OrderDetailID)
            {
                return BadRequest();
            }

            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            orderDetail.OrderID = dto.OrderID;
            orderDetail.ProductID = dto.ProductID;
            orderDetail.Quantity = dto.Quantity;
            orderDetail.UnitPrice = dto.UnitPrice;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!OrderDetailExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/OrderDetailDtoes
        [HttpPost]
        public async Task<IActionResult> PostOrderDetailDto(OrderDetailDto orderDetailDto)
        {
            var orderDetail = new OrderDetail
            {
                OrderDetailID = orderDetailDto.OrderDetailID,
                OrderID = orderDetailDto.OrderID,
                ProductID = orderDetailDto.ProductID,
                Quantity = orderDetailDto.Quantity,
                UnitPrice = orderDetailDto.UnitPrice
            };

            await _context.AddAsync(orderDetail);
            await _context.SaveChangesAsync();

            return Ok(orderDetail);
        }

        // DELETE: api/OrderDetailDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetailDto(int id)
        {
            if (_context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderDetailExists(int id)
        {
            return (_context.OrderDetails?.Any(e => e.OrderDetailID == id)).GetValueOrDefault();
        }
    }
}
