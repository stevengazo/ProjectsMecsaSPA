using System.ComponentModel.DataAnnotations;

namespace ProjectsMecsaSPA.Model
{
    public class Commentary
    {
        [Key]
        public int CommentaryID { get; set; }
        public string CommentaryText { get; set; }
        public DateTime CreationDate { get; set; }
        public string Author { get; set; }
        public int ProjectId { get; set; }

        #region Relaciones
        public Project Project { get; set; }
        #endregion
    }
}
