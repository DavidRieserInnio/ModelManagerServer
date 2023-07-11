using ModelManagerServer.St4.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("Part_Permissions", Schema = "modelmanager")]
    public class PartPermission
    {
        [Key]
        public Guid PartPermission_Id { get; set; }
        public St4Permission Type { get; set; }
        public Guid RightGroupId { get; set; }

        public Guid Part_Id { get; set; }
        public int Part_Version { get; set; }

        [ForeignKey("Part_Id,Part_Version")]
        public Part Part { get; set; }
    }
}
