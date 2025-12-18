namespace ProjectsMecsaSPA.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public bool Deleted { get; set; }   
        public ICollection<SchEmpl> SchEmpls { get; set; }  
    }
}
