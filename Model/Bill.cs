using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsMecsaSPA.Model
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; }

        [Required(ErrorMessage = "El número de factura es obligatorio.")]
        [StringLength(50, ErrorMessage = "El número de factura no puede tener más de 50 caracteres.")]
        public string BillNumber { get; set; }

        [Required(ErrorMessage = "El importe es obligatorio.")]
        [Column(TypeName = "decimal(15, 3)")]
        [Range(0, double.MaxValue, ErrorMessage = "El importe debe ser un valor positivo.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "El importe es obligatorio.")]
        [Column(TypeName = "decimal(15, 3)")]
        [Range(0, double.MaxValue, ErrorMessage = "El importe debe ser un valor positivo.")]
        public decimal AmountOriginal { get; set; }

        [Required(ErrorMessage = "El importe es obligatorio.")]
        [Column(TypeName = "decimal(15, 3)")]
        [Range(0, double.MaxValue, ErrorMessage = "El importe debe ser un valor positivo.")]
        public decimal TypeOfChange { get; set; }

        [Required(ErrorMessage = "La moneda es obligatoria.")]
        [StringLength(10, ErrorMessage = "La moneda no puede tener más de 10 caracteres.")]
        public string Currency { get; set; }

        [StringLength(500, ErrorMessage = "La nota no puede tener más de 500 caracteres.")]
        public string Note { get; set; }
        [Required( ErrorMessage = "Debe Indicar el numero de tarea")]
        public int TaskNumber { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio.")]
        [StringLength(100, ErrorMessage = "El autor no puede tener más de 100 caracteres.")]
        public string Author { get; set; }

        [StringLength(50, ErrorMessage = "El ID del autor no puede tener más de 50 caracteres.")]
        public string AuthorId { get; set; }

        [StringLength(100, ErrorMessage = "El último editor no puede tener más de 100 caracteres.")]
        public string LastEditor { get; set; }

        [Required(ErrorMessage = "La fecha de edición es obligatoria.")]
        public DateTime LastEditionDate { get; set; }

        [Required(ErrorMessage = "La fecha de factura es obligatoria.")]
        public DateTime BillDate { get; set; }

        public Project? Project { get; set; }

        [Required(ErrorMessage = "El ID del proyecto es obligatorio.")]
        public int ProjectId { get; set; }

        public ICollection<BillFile> BillFiles { get; set; }

    }
}
