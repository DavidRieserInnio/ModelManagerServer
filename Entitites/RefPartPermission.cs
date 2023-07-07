using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("REF_Part_Permission", Schema = "modelmanager")]
    public class RefPartPermission
    {
        [ForeignKey(nameof(Part))]
        public Guid Part_Id { get; set; }
        [ForeignKey(nameof(PartPermission))]
        public Guid Permission_Id { get; set; }

        public virtual Part Part { get; set; }
        public virtual PartPermission PartPermission { get; set; }
    }
}
