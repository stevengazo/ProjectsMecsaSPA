using System.ComponentModel.DataAnnotations;

namespace ProjectsMecsaSPA.Model
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int DNI { get; set; }

        #region Relaciones
        public ICollection<Project> Projects { get; set; }
        #endregion
    }
}
