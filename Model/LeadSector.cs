using System.ComponentModel.DataAnnotations;

namespace ProjectsMecsaSPA.Model
{
    public class LeadSector
    {
        [Key]
        public int LeadSectorId { get; set; }   
        public string Name { get; set; }
        public ICollection<Lead>? Leads { get; set; }
    }
}
