using e_store.Models;
using System.ComponentModel.DataAnnotations;

namespace e_store.Dto
{
    public class CategoryDto
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }

    }
}
