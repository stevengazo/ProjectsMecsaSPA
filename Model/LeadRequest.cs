using System.ComponentModel.DataAnnotations;

namespace ProjectsMecsaSPA.Model
{
    public class LeadRequest
    {
        [Key]
        public int LeadRequestId { get; set; }
        public string RequestName { get; set; }
        public bool IsDeleted { get; set; }

        public int LeadSectorId { get; set; }

        public ICollection<Lead>? Leads { get; set; }
    }
}
