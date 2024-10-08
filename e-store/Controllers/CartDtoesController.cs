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
    public class CartDtoesController : ControllerBase
    {
        private readonly EStoreDbContext _context;

        public CartDtoesController(EStoreDbContext context)
        {
            _context = context;
        }

        // GET: api/CartDtoes
        [HttpGet]
        public IQueryable<CartDto> GetAll()
        {
            var cartItems = from c in _context.Carts
                            select new CartDto()
                            {
                                CartID = c.CartID,
                                CustomerID=c.CustomerID,
                                FirstName=c.Customer.FirstName,
                                LastName=c.Customer.LastName,
                                
                            };
            return cartItems;
        }

        // GET: api/CartDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartDto>> GetCartDto(int id)
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            var cartDto = new CartDto
            {
                CartID = cart.CartID,
               CustomerID=cart.CustomerID,
            };

            return cartDto;
        }

        // PUT: api/CartDtoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, CartDto dto)
        {
            if (id != dto.CartID)
            {
                return BadRequest();
            }

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            cart.CartID = dto.CartID;
           


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CartDtoExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/CartDtoes
        [HttpPost]
        public async Task<IActionResult> PostCartDto(CartDto cartDto)
        {
            var dto = new Cart
            {
                CartID = cartDto.CartID,
                CustomerID=cartDto.CustomerID,
                
             
            };

            await _context.AddAsync(dto);
            await _context.SaveChangesAsync();

            return Ok(dto);
        }

        // DELETE: api/CartDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartDto(int id)
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartDtoExists(int id)
        {
            return (_context.Carts?.Any(e => e.CartID == id)).GetValueOrDefault();
        }
    }
}
