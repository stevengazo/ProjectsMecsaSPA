namespace ProjectsMecsaSPA.Model
{
    public class Type
    {
        public int TypeId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        #region Relaciones
        public ICollection<Project> Projects { get; set; }
        #endregion
    }

}
