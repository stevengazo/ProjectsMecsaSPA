namespace ProjectsMecsaSPA.DTO
{
    public class Car
    {
        public Guid CarId { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string FuelType { get; set; }
        public string Plate { get; set; }
        public string ImagePath { get; set; }
        public DateTime AdquisitionDate { get; set; }
        public string Vin { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal? Lenght { get; set; }
        public decimal Weight { get; set; }
        public string Notes { get; set; }
        public bool Deleted { get; set; }
        public bool IsAvailable { get; set; }
        public int Year { get; set; }

        // Relaciones
        public object Services { get; set; }
        public object EntryExitReports { get; set; }
        public object IssueReports { get; set; }
        public object CrashReports { get; set; }
        public object VehicleReturns { get; set; }
        public object Reminders { get; set; }
        public object VehicleAttachments { get; set; }
        public List<object> Bookings { get; set; }
    }

}
