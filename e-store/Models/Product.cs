using System.ComponentModel.DataAnnotations;

namespace e_store.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductReview> ProductReviews { get; set; }

        public ICollection<Favorite> Favorites { get; set; } 

  
        public string ImageUrl { get; set; }



    }
}
