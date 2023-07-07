using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("Part", Schema = "modelmanager")]
    public class Part
    {
        [Key]
        public Guid Part_Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public int Type { get; set; }
        public string ElementText { get; set; }
        [ForeignKey(nameof(Rule))]
        protected Guid? Rule_Id { get; set; }
        [ForeignKey(nameof(Enum))]
        protected Guid? Enum_Id { get; set; }

        public virtual ICollection<Model> Models { get; set; }
        public virtual Rule? Rule { get; set; }
        public virtual Enum? Enum { get; set; }
        public virtual ICollection<PartProperty> PartProperties { get; set; }
        public virtual ICollection<PartPermission> PartPermissions { get; set; }

        public virtual ICollection<RefModelPart> RefModelParts { get; set; }
        public virtual ICollection<RefPartPermission> RefPartPermissions { get; set; }
        public virtual ICollection<RefPartProperty> RefPartProperties { get; set; }

        public void CreateReferences()
        {
            this.Enum?.CreateReferences();

            this.RefPartPermissions = this.PartPermissions.Select((p, i) => new RefPartPermission()
            {
                Part = this,
                PartPermission = p,
            }).ToList();

            this.RefPartProperties = this.PartProperties.Select((p, i) => new RefPartProperty()
            {
                Part = this,
                PartProperty = p,
                PropertyPosition = i,
            }).ToList();
        }
    }
}
