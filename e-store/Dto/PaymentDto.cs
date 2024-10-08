using System.ComponentModel.DataAnnotations;

namespace e_store.Dto
{
    public class PaymentDto
    {
        [Key]
        public int PaymentID { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int OrderID { get; set; }
    }
}
