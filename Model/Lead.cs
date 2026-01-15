using System.ComponentModel.DataAnnotations;

namespace ProjectsMecsaSPA.Model
{
    public class Lead
    {
        [Key]
        public int LeadId { get; set; }
        public int CustomerName { get; set; }
        public string ContactName { get; set; }
        public string Description { get; set; }
        public int DealNumber { get; set; }
        public int Prioritized { get; set; } = 0;
        public bool IsNew { get; set; }
        public bool ContactedBySeller { get; set; }
        public bool isCalled { get; set; }
        public bool isDeleted { get; set; }
        public string Country { get; set; }

        public string CreatedBy { get; set; }   
        public DateTime CreatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Seller
        public int SellerId { get; set; }
        public Seller Seller { get; set; }  

        // Lead State
        public int LeadStateId { get; set; }
        public LeadState LeadState { get; set; }

        // Request type
        public int LeadRequestId { get; set; }
        public LeadRequest LeadRequest { get; set; }

        // Origin
        public LeadOrigin Origin { get; set; }
        public int LeadOriginId { get; set; }
        /// Notes
        public ICollection<LeadNotes> Notes { get; set; }

    }
}
