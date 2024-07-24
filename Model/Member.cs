using System.ComponentModel.DataAnnotations;

namespace ProjectsMecsaSPA.Model
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        #region Relaciones
        public ICollection<ProjectMember> ProjectMembers { get; set; }
        #endregion
    }
}
