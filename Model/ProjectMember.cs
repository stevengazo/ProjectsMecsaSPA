namespace ProjectsMecsaSPA.Model
{
    public class ProjectMember
    {
        public int ProjectMemberId { get; set; }
        public int MemberId { get; set; }
        public int ProjectId { get; set; }
        public int MemberTypeId { get; set; }
        public bool Notify { get; set; }

        #region Relaciones
        public Member Member { get; set; }
        public Project Project { get; set; }
        public MemberType MemberType { get; set; }
        #endregion
    }
}
