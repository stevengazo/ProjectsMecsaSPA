using System.ComponentModel.DataAnnotations;

namespace ProjectsMecsaSPA.Model
{
    public class TypeModel
    {
        [Key]
        public int TypeId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        #region Relaciones
        public ICollection<Project> Projects { get; set; }
        #endregion
    }

}
