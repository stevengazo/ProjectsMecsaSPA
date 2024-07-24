namespace ProjectsMecsaSPA.Model
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        #region Relaciones
        public ICollection<ProjectMember> ProjectMembers { get; set; }
        #endregion
    }
}
