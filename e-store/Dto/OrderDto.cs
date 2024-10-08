using System.ComponentModel.DataAnnotations;

namespace e_store.Dto
{
    public class OrderDto
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerID { get; set; }
    }
}
