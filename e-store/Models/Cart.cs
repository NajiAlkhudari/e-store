using System.ComponentModel.DataAnnotations;

namespace e_store.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
        public int CustomerID { get; set; }  // يمكن أن يكون اختياريًا للمستخدمين غير المسجلين
        public Customer Customer { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
