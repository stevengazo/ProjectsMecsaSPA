namespace ProjectsMecsaSPA.Model
{
    public class FileModel
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }
        public DateTime Creation { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public int ProjectId { get; set; }

        #region Relaciones
        public Project Project { get; set; }
        #endregion
    }
}
