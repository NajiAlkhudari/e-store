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
    public class ProductDtoesController : ControllerBase
    {
        private readonly EStoreDbContext _context;

        public ProductDtoesController(EStoreDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductDtoes
     

        [HttpGet]
        public IQueryable<ProductDto> GetAll()
        {
            var product = from b in _context.Products
                          select new ProductDto()
                          {
                              ProductID = b.ProductID,
                              Name = b.Name,
                              Price = b.Price,
                              StockQuantity = b.StockQuantity,
                              Description = b.Description,
                              CategoryID = b.CategoryID,
                              ImageUrl = b.ImageUrl
                          };
            return product;
        }

        [Route("Search")]
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProducts(string query)
        {
            IQueryable<Product> productsQuery = _context.Products;

            if (!string.IsNullOrEmpty(query))
            {
                // تحويل الاسم والنص المدخل إلى أحرف صغيرة
                productsQuery = productsQuery.Where(p => p.Name.ToLower().Contains(query.ToLower()));
            }

            var products = await productsQuery.Select(p => new ProductDto
            {
                ProductID = p.ProductID,
                Name = p.Name,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                Description = p.Description,
                CategoryID = p.CategoryID,
                ImageUrl = p.ImageUrl
            }).ToListAsync();

            return Ok(products);
        }



        // GET: api/ProductDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductDto(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // تحويل الكائن category إلى CategoryDto
            var productDto = new ProductDto
            {
                CategoryID=product.CategoryID,
                Name = product.Name,
                Price=product.Price,
                StockQuantity=product.StockQuantity,
                Description=product.Description,

                // أضف الحقول الأخرى المطلوبة للتحويل
            };

            return productDto;
        }


        // PUT: api/ProductDtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto dto)
        {
            if (id != dto.ProductID)
            {
                return BadRequest();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Description = dto.Description;
            product.StockQuantity = dto.StockQuantity;
            product.CategoryID = dto.CategoryID;



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ProdutDtoExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool ProdutDtoExists(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/ProductDtoes
     

        [HttpPost]
        public async Task<IActionResult> PostProductDto([FromForm] ProductDto productDto, IFormFile imageFile)
        {
            string imagePath = null;

            // حفظ الصورة
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // التأكد من إعادة رابط الصورة
                imagePath = "http://localhost:38951/images/" + fileName;
            }

            var dto = new Product()
            {
                ProductID = productDto.ProductID,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity,
                CategoryID = productDto.CategoryID,
                ImageUrl = imagePath // التأكد من إضافة مسار الصورة
            };

            await _context.AddAsync(dto);
            await _context.SaveChangesAsync();

            return Ok(dto); // إرجاع الكائن الذي يحتوي على البيانات بما في ذلك مسار الصورة
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductDto(int id)

        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var productyDto = await _context.Products.FindAsync(id);
            if (productyDto == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productyDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool ProductDtoExists(int id)
        {
            return (_context.ProductDto?.Any(e => e.ProductID == id)).GetValueOrDefault();
        }
    }
}
