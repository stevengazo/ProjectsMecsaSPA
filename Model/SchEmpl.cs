namespace ProjectsMecsaSPA.Model
{
    public class SchEmpl
    {
        public int SchEmplId { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }

}
