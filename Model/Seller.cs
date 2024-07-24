namespace ProjectsMecsaSPA.Model
{
    public class Seller
    {
        public int SellerId { get; set; }
        public string SellerName { get; set; }
        public string Email { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
