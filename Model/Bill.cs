using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsMecsaSPA.Model
{
    public class Bill
    {
        public int BillId { get; set; }
        public string BillNumber { get; set; }

        [Column(TypeName = "decimal(15, 3)")]
        public decimal Amount { get; set; } 
        public string Currency { get; set; }
        public string Note { get; set; }
        public string Author { get; set; }
        public string AuthorId { get; set; }
        public string LastEditor { get; set; }
        public DateTime LastEditionDate { get; set; }
        public DateTime BillDate { get; set; }
        public Project Project {  get; set; }
        public int ProjectId { get; set; }
    }

}
