using System.ComponentModel.DataAnnotations;

namespace ProjectsMecsaSPA.Model
{
    public class LeadOrigin
    {
        [Key]
        public int LeadOriginId { get; set; }
        public string LeadOriginName { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Lead>  Leads { get; set; }
    }
}
