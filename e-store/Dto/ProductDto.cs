using System.ComponentModel.DataAnnotations;

namespace e_store.Dto
{
    public class ProductDto
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryID { get; set; }
        public string ImageUrl { get; set; }


    }
}
