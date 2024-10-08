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
    public class CustomerDtoesController : ControllerBase
    {
        private readonly EStoreDbContext _context;

        public CustomerDtoesController(EStoreDbContext context)
        {
            _context = context;
        }

        // GET: api/CustomerDtoes
        [HttpGet]
        public IQueryable<CustomerDto>GetAll()
        {
            var customer = from b in _context.Customers
                           select new CustomerDto()
                           {
                               CustomerID = b.CustomerID,
                               FirstName = b.FirstName,
                               LastName=b.LastName,
                               Address = b.Address,
                               Email = b.Email,
                               PhoneNumber = b.PhoneNumber
                           };
                        return customer;
        }

        // GET: api/CustomerDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerDto(int id)
        {
          if (_context.CustomerDto == null)
          {
              return NotFound();
          }
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            var customerDto = new CustomerDto
            {
                CustomerID = customer.CustomerID,
                FirstName = customer.FirstName,
                LastName=customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,
                Email = customer.Address,
            };

            return customerDto;
        }

        // PUT: api/CustomerDtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerDto(int id, CustomerDto dto)
        {
            if (id != dto.CustomerID)
            {
                return BadRequest();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            customer.FirstName = dto.FirstName;
            customer.LastName = dto.LastName;
            customer.Address = dto.Address;
            customer.Email = dto.Email;
            customer.PhoneNumber = dto.PhoneNumber;
  

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerDtoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool CustomerDtoExists(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/CustomerDtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult>PostCustomerDto(CustomerDto customerDto)
        {
            var dto = new Customer()

            {
              FirstName=customerDto.FirstName,
              LastName=customerDto.LastName,
              Email=customerDto.Email,
              PhoneNumber=customerDto.PhoneNumber,
              Address=customerDto.Address,
              CustomerID=customerDto.CustomerID,

            };
            await _context.AddAsync(dto);
            _context.SaveChanges();
            return Ok(dto);
        }

        // DELETE: api/CustomerDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerDto(int id)

        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customerDto = await _context.Customers.FindAsync(id);
            if (customerDto == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customerDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool ProductDtoExists(int id)
        {
            return (_context.ProductDto?.Any(e => e.ProductID == id)).GetValueOrDefault();
        }
    }
}
