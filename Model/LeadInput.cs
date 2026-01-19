using System.ComponentModel.DataAnnotations;

namespace ProjectsMecsaSPA.Model
{
    public class LeadInput
    {
        [Key]
        public int LeadInputId { get; set; }
        public string Name { get; set; }
        public ICollection<Lead> Leads { get; set; }
    }
}
