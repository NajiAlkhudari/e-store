using e_store.Data;
using e_store.Dto;
using e_store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace e_store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReviewController : ControllerBase
    {
        private readonly EStoreDbContext _context;

        public ProductReviewController(EStoreDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductReview
        [HttpGet]
        public IQueryable<ProductReviewDto> GetAll()
        {
            var reviews = from r in _context.ProductReviews
                          select new ProductReviewDto
                          {
                              ProductReviewID = r.ProductReviewID,
                              ProductID = r.ProductID,
                              CustomerID = r.CustomerID,
                              Rating = r.Rating,
                              ReviewText = r.ReviewText,
                              ReviewDate = r.ReviewDate,
                          };
            return reviews;
        }

        // GET: api/ProductReview/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReviewDto>> GetProductReview(int id)
        {
            if (_context.ProductReviews == null)
            {
                return NotFound();
            }

            var review = await _context.ProductReviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            var reviewDto = new ProductReviewDto
            {
                ProductReviewID = review.ProductReviewID,
                ProductID = review.ProductID,
                CustomerID = review.CustomerID,
                Rating = review.Rating,
                ReviewText = review.ReviewText,
                ReviewDate = review.ReviewDate,
            };

            return reviewDto;
        }

        // POST: api/ProductReview
        [HttpPost]
        public async Task<IActionResult> PostProductReview(ProductReviewDto reviewDto)
        {
            var review = new ProductReview()
            {
                ProductID = reviewDto.ProductID,
                CustomerID = reviewDto.CustomerID,
                Rating = reviewDto.Rating,
                ReviewText = reviewDto.ReviewText,
                ReviewDate = reviewDto.ReviewDate,
            };

            await _context.ProductReviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return Ok(review);
        }

        // PUT: api/ProductReview/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductReview(int id, ProductReviewDto reviewDto)
        {
            if (id != reviewDto.ProductReviewID)
            {
                return BadRequest();
            }

            var review = await _context.ProductReviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            review.ProductID = reviewDto.ProductID;
            review.CustomerID = reviewDto.CustomerID;
            review.Rating = reviewDto.Rating;
            review.ReviewText = reviewDto.ReviewText;
            review.ReviewDate = reviewDto.ReviewDate;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ProductReviewExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/ProductReview/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductReview(int id)
        {
            if (_context.ProductReviews == null)
            {
                return NotFound();
            }

            var review = await _context.ProductReviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.ProductReviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductReviewExists(int id)
        {
            return _context.ProductReviews.Any(e => e.ProductReviewID == id);
        }
    }
}
