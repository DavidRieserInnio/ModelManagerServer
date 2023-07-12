using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entities
{
    [Table("Parts", Schema = "modelmanager")]
    public class Part
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string ElementText { get; set; }
        public Guid? Rule_Id { get; set; }
        public Guid? Enum_Id { get; set; }
        public int? Enum_Version { get; set; }

        public virtual IList<Model> Models { get; set; }
        public virtual Rule? Rule { get; set; }
        public virtual Enum? Enum { get; set; }
        public virtual IList<PartProperty> PartProperties { get; set; }
        public virtual IList<PartPermission> PartPermissions { get; set; }

        public virtual IList<RefModelPart> RefModelsParts { get; set; }
    }
}
