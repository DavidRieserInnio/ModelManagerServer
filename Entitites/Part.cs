using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("Parts", Schema = "modelmanager")]
    [PrimaryKey(nameof(Part_Id), nameof(Version))]
    public class Part
    {
        public Guid Part_Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string ElementText { get; set; }
        [ForeignKey(nameof(Rule))]
        protected Guid? Rule_Id { get; set; }
        protected Guid? Enum_Id { get; set; }
        public int? Enum_Version { get; set; }

        public virtual Rule? Rule { get; set; }
        [ForeignKey("Enum_Id,Enum_Version")]
        public virtual Enum? Enum { get; set; }
        public virtual ICollection<PartProperty> PartProperties { get; set; }
        public virtual ICollection<PartPermission> PartPermissions { get; set; }
    }
}
