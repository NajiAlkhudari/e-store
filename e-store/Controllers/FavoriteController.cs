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
    public class FavoriteController : ControllerBase
    {
        private readonly EStoreDbContext _context;

        public FavoriteController(EStoreDbContext context)
        {
            _context = context;
        }

        // GET: api/Favorites
        [HttpGet]
        public IQueryable<FavoritesDto> GetAll()
        {
            var favorites = from f in _context.Favorites
                            select new FavoritesDto()
                            {
                                FavoriteID = f.FavoriteID,
                                CustomerID = f.CustomerID,
                                ProductID = f.ProductID
                            };
            return favorites;
        }

        // GET: api/Favorites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FavoritesDto>> GetFavorite(int id)
        {
            if (_context.Favorites == null)
            {
                return NotFound();
            }

            var favorite = await _context.Favorites.FindAsync(id);

            if (favorite == null)
            {
                return NotFound();
            }

            var favoriteDto = new FavoritesDto
            {
                FavoriteID = favorite.FavoriteID,
                CustomerID = favorite.CustomerID,
                ProductID = favorite.ProductID
            };

            return favoriteDto;
        }

        // POST: api/Favorites
        [HttpPost]
        public async Task<IActionResult> PostFavorite(FavoritesDto favoriteDto)
        {
            var favorite = new Favorite()
            {
                CustomerID = favoriteDto.CustomerID,
                ProductID = favoriteDto.ProductID
            };

            await _context.Favorites.AddAsync(favorite);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFavorite), new { id = favorite.FavoriteID }, favoriteDto);
        }

        // PUT: api/Favorites/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFavorite(int id, FavoritesDto favoriteDto)
        {
            if (id != favoriteDto.FavoriteID)
            {
                return BadRequest();
            }

            var favorite = await _context.Favorites.FindAsync(id);
            if (favorite == null)
            {
                return NotFound();
            }

            favorite.CustomerID = favoriteDto.CustomerID;
            favorite.ProductID = favoriteDto.ProductID;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!FavoriteExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Favorites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            if (_context.Favorites == null)
            {
                return NotFound();
            }

            var favorite = await _context.Favorites.FindAsync(id);
            if (favorite == null)
            {
                return NotFound();
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavoriteExists(int id)
        {
            return (_context.Favorites?.Any(e => e.FavoriteID == id)).GetValueOrDefault();
        }
    }
}
