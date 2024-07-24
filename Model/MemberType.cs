namespace ProjectsMecsaSPA.Model
{
    public class MemberType
    {
        public int MemberTypeId { get; set; }
        public string Name { get; set; }

        #region Relaciones
        public ICollection<ProjectMember> ProjectMembers { get; set; }
        #endregion
    }
}
