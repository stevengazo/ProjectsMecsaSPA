using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsMecsaSPA.Model
{
    public class Offer
    {
        [Key]
        public int OfferId { get; set; }
        [Required(ErrorMessage = "La fecha de registro es requerida")]
        public DateTime Creation { get; set; }
        [Required(ErrorMessage = "El cliente es requerido")]
        [StringLength(200, ErrorMessage = "El nombre del cliente no puede exceder los 200 caracteres")]
        public string Customer { get; set; }
        [Required(ErrorMessage = "La descripción es requerida")]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres")]
        public string Description { get; set; }
        public string ContactType { get; set; }
        [Required(ErrorMessage = "El tipo es requerido")]
        public string Type { get; set; }
        [Required(ErrorMessage = "El estado es requerido")]
        public string State { get; set; }
        public bool RequiredDDCE { get; set; }
        public bool RequiredLightningStrike { get; set; }
        public bool RequireTower { get; set; }
        public bool RequireSPAT { get; set; }
        public bool RequireSurgeProtector { get; set; }
        public bool RequireOther { get; set; }
        [Required(ErrorMessage = "El monto es requerido")]
        [Range(0.0, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
        [Column(TypeName = "decimal(15, 3)")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "El campo Cotizado Por es requerido")]
        [StringLength(100, ErrorMessage = "El nombre del cotizador no puede exceder los 100 caracteres")]
        public string CalculateBy { get; set; }
        [Required(ErrorMessage = "El autor es requerido")]
        [StringLength(100, ErrorMessage = "El nombre del autor no puede exceder los 100 caracteres")]
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public string Responsible { get; set; }
        public string ResponsibleId { get; set; }

        #region Relaciones
        // Aquí se pueden agregar las relaciones si es necesario
        #endregion
    }
}