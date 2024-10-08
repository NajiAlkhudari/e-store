using Microsoft.AspNetCore.Identity;

namespace e_store.Models
{
    public class ApplicationUser:IdentityUser
    {
        public String ?FirstName { get; set; }
        public String ?LastName { get; set; }
        public String ?ProfileImage { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public String ?Country{ get; set; }
        public String ?Gender { get; set; }
        public String ?Address { get; set; }
        public String ?City { get; set; }
        public int Postcode { get; set; }
        public String? PassportNumber { get; set; }
        public String? IssuingCountry { get; set; }
        public DateTime? PassportExpiryDate { get; set; }
       
    }
}
