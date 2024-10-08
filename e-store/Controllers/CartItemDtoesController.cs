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
    public class CartItemDtoesController : ControllerBase
    {
        private readonly EStoreDbContext _context;

        public CartItemDtoesController(EStoreDbContext context)
        {
            _context = context;
        }

        // GET: api/CartItemDtoes
        [HttpGet]
        public IQueryable<CartItemDto> GetAll()
        {
            var cartItems = from c in _context.CartItems
                            select new CartItemDto()
                            {
                                CartItemID = c.CartItemID,
                                CartID = c.CartID,
                                ProductID = c.ProductID,
                                Quantity=c.Quantity
                                
                            };
            return cartItems;
        }

        // GET: api/CartItemDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartItemDto>> GetCartItemDto(int id)
        {
            if (_context.CartItems == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems.FindAsync(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            var cartItemDto = new CartItemDto
            {
                CartItemID = cartItem.CartItemID,
                CartID = cartItem.CartID,
                ProductID = cartItem.ProductID,
                Quantity = cartItem.Quantity
            };

            return cartItemDto;
        }

        // PUT: api/CartItemDtoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCartItem(int id, CartItemDto dto)
        {
            if (id != dto.CartItemID)
            {
                return BadRequest();
            }

            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            cartItem.CartID = dto.CartID;
            cartItem.ProductID = dto.ProductID;
            cartItem.Quantity = dto.Quantity;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CartItemDtoExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/CartItemDtoes
        [HttpPost]
        public async Task<IActionResult> PostCartItemDto(CartItemDto cartItemDto)
        {
            var cartItem = new CartItem
            {
                CartItemID = cartItemDto.CartItemID,
                CartID = cartItemDto.CartID,
                ProductID = cartItemDto.ProductID,
                Quantity = cartItemDto.Quantity
            };

            await _context.AddAsync(cartItem);
            await _context.SaveChangesAsync();

            return Ok(cartItem);
        }

        // DELETE: api/CartItemDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItemDto(int id)
        {
            if (_context.CartItems == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartItemDtoExists(int id)
        {
            return (_context.CartItems?.Any(e => e.CartItemID == id)).GetValueOrDefault();
        }
    }
}
