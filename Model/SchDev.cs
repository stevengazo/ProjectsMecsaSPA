namespace ProjectsMecsaSPA.Model
{
    public class SchDev
    {
        public int SchDevId { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }

        public bool Deleted { get; set; } = false;
    }

}
