using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsMecsaSPA.Model
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede exceder los 100 caracteres.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El número de tarea es obligatorio.")]
        [StringLength(50, ErrorMessage = "El número de tarea no puede exceder los 50 caracteres.")]
        public string TaskNumber { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria.")]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio.")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio.")]
        public int CustomerId { get; set; }

        [StringLength(200, ErrorMessage = "La ubicación no puede exceder los 200 caracteres.")]
        public string Ubication { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Column(TypeName = "decimal(15, 3)")]
        [Range(0, 999999999999999.999, ErrorMessage = "El monto debe estar entre 0 y 999999999999999.999.")]
        public decimal Amount { get; set; }

        [StringLength(50, ErrorMessage = "El OC no puede exceder los 50 caracteres.")]
        public string OC { get; set; }

        [Required(ErrorMessage = "La fecha de OC es obligatoria.")]
        public DateTime OCDate { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsCompleted { get; set; }
        public bool Isfactured { get; set; }

        [Required(ErrorMessage = "El vendedor es obligatorio.")]
        public int SellerId { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public int StateId { get; set; }

        [StringLength(10, ErrorMessage = "El tipo de moneda no puede exceder los 3 caracteres.")]
        public string CurrencyType { get; set; }

        [Column(TypeName = "decimal(15, 3)")]
        public decimal TypeOfChange { get; set; }

        [StringLength(100, ErrorMessage = "La provincia no puede exceder los 100 caracteres.")]
        public string Province { get; set; }

        #region Relaciones
        public Seller Seller { get; set; }
        public TypeModel Type { get; set; }
        public Customer Customer { get; set; }
        public State State { get; set; }
        public ICollection<FileModel> Files { get; set; }
        public ICollection<Commentary> Commentaries { get; set; }
        public ICollection<Bill> Bills { get; set; }
        #endregion
    }
}
