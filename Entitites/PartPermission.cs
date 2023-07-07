using ModelManagerServer.St4.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("Part_Permission", Schema = "modelmanager")]
    public class PartPermission
    {
        [Key]
        public Guid PartPermission_Id { get; set; }
        public St4Permission Type { get; set; }
        public Guid RightGroupId { get; set; }

        public ICollection<Part> Parts { get; set; }
        public virtual ICollection<RefPartPermission> RefPartPermissions { get; set; }
    }
}
