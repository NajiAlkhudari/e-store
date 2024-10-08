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
    public class PaymentDtoesController : ControllerBase
    {
        private readonly EStoreDbContext _context;

        public PaymentDtoesController(EStoreDbContext context)
        {
            _context = context;
        }

        // GET: api/PaymentDtoes
        [HttpGet]
        public IQueryable<PaymentDto> GetAll()
        {
            var payments = from p in _context.Payments
                           select new PaymentDto()
                           {
                               PaymentID = p.PaymentID,
                               OrderID = p.OrderID,
                               Amount = p.Amount,
                               PaymentDate = p.PaymentDate,
                               PaymentMethod = p.PaymentMethod
                               
                           };
            return payments;
        }

        // GET: api/PaymentDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetPaymentDto(int id)
        {
            if (_context.Payments == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            var paymentDto = new PaymentDto
            {
                PaymentID = payment.PaymentID,
                OrderID = payment.OrderID,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod
            };

            return paymentDto;
        }

        // PUT: api/PaymentDtoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, PaymentDto dto)
        {
            if (id != dto.PaymentID)
            {
                return BadRequest();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            payment.OrderID = dto.OrderID;
            payment.Amount = dto.Amount;
            payment.PaymentDate = dto.PaymentDate;
            payment.PaymentMethod = dto.PaymentMethod;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PaymentExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/PaymentDtoes
        [HttpPost]
        public async Task<IActionResult> PostPaymentDto(PaymentDto paymentDto)
        {
            var payment = new Payment
            {
                PaymentID = paymentDto.PaymentID,
                OrderID = paymentDto.OrderID,
                Amount = paymentDto.Amount,
                PaymentDate = paymentDto.PaymentDate,
                PaymentMethod = paymentDto.PaymentMethod
            };

            await _context.AddAsync(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPaymentDto), new { id = payment.PaymentID }, payment);
        }

        // DELETE: api/PaymentDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentDto(int id)
        {
            if (_context.Payments == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentExists(int id)
        {
            return (_context.Payments?.Any(e => e.PaymentID == id)).GetValueOrDefault();
        }
    }
}
