using System.ComponentModel.DataAnnotations;

namespace ProjectsMecsaSPA.Model
{
    public class LeadNotes
    {
        [Key]
        public int LeadNoteId { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public Lead Lead { get; set; }
        public int LeadId { get; set; }
    }
}
