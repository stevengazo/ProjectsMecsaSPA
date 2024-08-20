using System.ComponentModel.DataAnnotations;

namespace ProjectsMecsaSPA.Model
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El tipo no puede exceder los 50 caracteres.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [Range(1, 99999999999, ErrorMessage = "El DNI debe estar entre 1,0000,0000 y 999,9999,9999.")]
        public long DNI { get; set; }

        #region Relaciones
        public ICollection<Project> Projects { get; set; }
        #endregion
    }
}
