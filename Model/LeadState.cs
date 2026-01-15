using System.ComponentModel.DataAnnotations;

namespace ProjectsMecsaSPA.Model
{
    public class LeadState
    {
        [Key]
        public int LeadStateId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public int Order { get; set; }

        public bool Priority { get; set; } = false;

        public ICollection<Lead> Leads { get; set; }
    }
}
