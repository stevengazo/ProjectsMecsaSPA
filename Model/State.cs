using System.ComponentModel.DataAnnotations;

namespace ProjectsMecsaSPA.Model
{
    public class State
    {
        [Key]
        public int StateId { get; set; }
        public string StateName { get; set; }
        public bool SendNotification { get; set; }
        public bool IsDeleted { get; set; }

        #region Relaciones
        public ICollection<Project> Projects { get; set; }
        #endregion
    }

}
