using System.ComponentModel.DataAnnotations;

namespace ProjectsMecsaSPA.Model
{
    public class BillFile
    {
        [Key]
        public int BillFileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public DateTime? Creation { get; set; }
        public int BillId { get; set; }
        public Bill Bill { get; set; }
    }
}
