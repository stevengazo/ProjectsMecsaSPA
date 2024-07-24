using System.ComponentModel.DataAnnotations;

namespace ProjectsMecsaSPA.Model
{
    public class MemberType
    {
        [Key]
        public int MemberTypeId { get; set; }
        public string Name { get; set; }

        #region Relaciones
        public ICollection<ProjectMember> ProjectMembers { get; set; }
        #endregion
    }
}
