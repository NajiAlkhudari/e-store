using e_store.Models;
using System.ComponentModel.DataAnnotations;

namespace e_store.Dto
{
    public class CartDto
    {
        [Key]
        public int CartID { get; set; }
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


    }
}
