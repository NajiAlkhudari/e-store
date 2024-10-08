using System.ComponentModel.DataAnnotations;

namespace e_store.Dto
{
    public class ProductReviewDto
    {
        [Key]
        public int ProductReviewID { get; set; }  
        public int ProductID { get; set; }        
        public int CustomerID { get; set; }       
        public int Rating { get; set; }          
        public string ReviewText { get; set; }  
        public DateTime ReviewDate { get; set; }
    }
}
