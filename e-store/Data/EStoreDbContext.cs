
using e_store.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace e_store.Data
{
    public class EStoreDbContext : IdentityDbContext<ApplicationUser>
    {
        public EStoreDbContext(DbContextOptions<EStoreDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ProductReview> ProductReviews  { get; set; }
        public DbSet<Favorite> Favorites { get; set; }




        public DbSet<e_store.Dto.CategoryDto>? CategoryDto { get; set; }
        public DbSet<e_store.Dto.ProductDto>? ProductDto { get; set; }
        public DbSet<e_store.Dto.CartDto>? CartDto { get; set; }
        public DbSet<e_store.Dto.CartItemDto>? CartItemDto { get; set; }
        public DbSet<e_store.Dto.CustomerDto>? CustomerDto { get; set; }
        public DbSet<e_store.Dto.OrderDto>? OrderDto { get; set; }
        public DbSet<e_store.Dto.OrderDetailDto>? OrderDetailDto { get; set; }
        public DbSet<e_store.Dto.PaymentDto>? PaymentDto { get; set; }
    }
}
