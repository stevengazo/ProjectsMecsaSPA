using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsMecsaSPA.Model
{
    public class Offer
    {
        [Key]
        public int OfferId { get; set; }
        public DateTime Creation { get; set; }
        public string Customer { get; set; }
        public string Description { get; set; }
        public bool RequiredDDCE { get; set; }
        public bool RequiredLightningStrike { get; set; }
        public bool RequireTower { get; set; }
        public bool RequireSPAT { get; set; }
        public bool RequireSurgeProtector { get; set; }
        public bool RequireOther { get; set; }
        [Column(TypeName = "decimal(15, 3)")]
        public decimal Amount { get; set; }
        public string CalculateBy { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }

        #region Relaciones
        // Aquí se pueden agregar las relaciones si es necesario
        #endregion
    }
}
