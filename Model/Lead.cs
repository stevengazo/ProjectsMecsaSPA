using System.ComponentModel.DataAnnotations;

namespace ProjectsMecsaSPA.Model
{
    public class Lead
    {
        [Key]
        public int LeadId { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactName { get; set; }
        public string? Description { get; set; }
        public int DealNumber { get; set; }
        public string? Prioritized { get; set; } 
        public bool IsNew { get; set; }
        public bool isDeleted { get; set; }
        public string? Country { get; set; }

        public string? CreatedBy { get; set; }   
        public DateTime CreatedAt { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Seller
        public int SellerId { get; set; }
        public Seller? Seller { get; set; }  

        // Lead State
        public int LeadStateId { get; set; }
        public LeadState? LeadState { get; set; }

        // Request type
        public int LeadRequestId { get; set; }
        public LeadRequest? LeadRequest { get; set; }

        // Origin
        public LeadOrigin? Origin { get; set; }
        public int LeadOriginId { get; set; }

        // Input
        public LeadInput? Input { get; set; }
        public int LeadInputId { get; set; } 

        // Sector
        public LeadSector? Sector { get; set; }
        public int LeadSectorId { get; set; } 
        /// Notes
        public ICollection<LeadNotes> Notes { get; set; }

    }
}
