
namespace ProjectsMecsaSPA.Model
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public string InvoiceDescription { get; set; }
        public string CreationDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }

    }
}
