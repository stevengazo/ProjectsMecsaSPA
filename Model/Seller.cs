using System.ComponentModel.DataAnnotations;

namespace ProjectsMecsaSPA.Model
{
    public class Seller
    {
        [Key]
        public int SellerId { get; set; }
        public string SellerName { get; set; }
        public int DNI { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; } 
        public int Bitrix24Id { get; set; } 
        public bool IsActive { get; set; }
        public ICollection<Project> Projects { get; set; }

        public ICollection<Lead> Leads { get; set; }
    }
}
