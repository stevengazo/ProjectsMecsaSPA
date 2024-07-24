namespace ProjectsMecsaSPA.Model
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int TypeId { get; set; }
        public int CustomerId { get; set; }
        public string Ubication { get; set; }
        public decimal Amount { get; set; }
        public string OC { get; set; }
        public DateTime OCDate { get; set; }
        public bool IsDeleted { get; set; }
        public string Seller { get; set; }
        public int StateId { get; set; }

        #region Relaciones
        public Type Type { get; set; }
        public Customer Customer { get; set; }
        public State State { get; set; }
        public ICollection<FileModel> Files { get; set; }
        public ICollection<Commentary> Commentaries { get; set; }
        public ICollection<ProjectMember> ProjectMembers { get; set; }
        #endregion
    }
}
