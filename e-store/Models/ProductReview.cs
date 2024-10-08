using System.ComponentModel.DataAnnotations;

namespace e_store.Models
{
    public class ProductReview
    {
        [Key]
        public int ProductReviewID { get; set; }  // معرف التقييم
        public int ProductID { get; set; }        // معرف المنتج المرتبط بالتقييم
        public Product Product { get; set; }      // المنتج الذي يتم تقييمه
        public int CustomerID { get; set; }       // معرف العميل الذي قدم التقييم
        public Customer Customer { get; set; }    // العميل الذي قدم التقييم
        public int Rating { get; set; }           // التقييم (من 1 إلى 5)
        public string ReviewText { get; set; }    // نص المراجعة أو التعليق
        public DateTime ReviewDate { get; set; }  // تاريخ تقديم التقييم
    }
}
