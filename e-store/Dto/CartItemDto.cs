using System.ComponentModel.DataAnnotations;

namespace e_store.Dto
{
    public class CartItemDto
    {
        [Key]
        public int CartItemID { get; set; }
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
