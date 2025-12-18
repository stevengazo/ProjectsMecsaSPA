namespace ProjectsMecsaSPA.Model
{
    public class Schedule
    {
        public int ScheduleId { get; set; } 
        public string TaskName { get; set; } = string.Empty;
        public int DependencyId { get; set; } = 0;
        public DateTime Start { get; set; } = DateTime.Now;
        public DateTime End { get; set; } = DateTime.Now.AddDays(1);
        public string Notes { get; set; } = "";
        public string CarPlate { get; set; } = "";
        public string Car { get; set; } = "";
        public bool WeekendWork { get; set; } = false;
        public string Color { get; set; } = RandomColor();
        public bool Draft { get; set; }
        public bool Deleted { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public ICollection<SchEmpl> SchEmpls { get; set; }
        public ICollection<SchDev> SchDevs { get; set; }
        private static string RandomColor()
        {
            var rnd = new Random();
            return $"#{rnd.Next(0x1000000):X6}";
        }

    }
}
