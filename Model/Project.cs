
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

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
        [Required]
        public string Description { get; set; }

        [Required(ErrorMessage = "El número de tarea es obligatorio.")]
        [StringLength(50, ErrorMessage = "El número de tarea no puede exceder los 50 caracteres.")]
        public string TaskNumber { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria.")]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio.")]
        public int TypeId { get; set; }

        public int FolderIDB24 { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio.")]
        public int CustomerId { get; set; }

        [StringLength(200, ErrorMessage = "La ubicación no puede exceder los 200 caracteres.")]
        [Required(ErrorMessage = "El dato es obligatorio.")]
        public string Ubication { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Column(TypeName = "decimal(15, 3)")]
        [Range(0, 999999999999999.999, ErrorMessage = "El monto debe estar entre 0 y 999999999999999.999.")]
        public decimal Amount { get; set; }

        [StringLength(50, ErrorMessage = "El OC no puede exceder los 50 caracteres.")]
        [Required(ErrorMessage = "La orden de compra es requerido.")]
        public string OC { get; set; }

        public int TAX { get; set; }

        [Required(ErrorMessage = "La fecha de OC es obligatoria.")]
        public DateTime OCDate { get; set; }
        [MaxLength(100,ErrorMessage ="El tamaño no puede ser mayor a 100 caracteres")]
        [Required(ErrorMessage = "El campo no puede estar vacio")]
        public string OfferId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsCompleted { get; set; }
        public bool Isfactured { get; set; }
        public bool RequiredCoordination { get; set; } = true;

        [Required(ErrorMessage = "El vendedor es obligatorio.")]
        public int SellerId { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public int StateId { get; set; }

        [StringLength(10, ErrorMessage = "El tipo de moneda no puede exceder los 3 caracteres.")]
        [Required(ErrorMessage = "El tipo de moneda es requerido.")]
        public string CurrencyType { get; set; }

        [Column(TypeName = "decimal(15, 3)")]
        public decimal TypeOfChange { get; set; }

        [StringLength(100, ErrorMessage = "La provincia no puede exceder los 100 caracteres.")]
        [Required(ErrorMessage ="La provincia es requerida")]
        public string Province { get; set; }

        #region Relaciones
        public int CompanyId { get; set; }
        public Company Company { get; set; }
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
