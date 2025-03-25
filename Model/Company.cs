using System.ComponentModel.DataAnnotations;

namespace ProjectsMecsaSPA.Model
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
