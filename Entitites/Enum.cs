using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("Enum", Schema = "modelmanager")]
    public class Enum
    {
        [Key]
        public Guid Enum_Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public Guid? ParentId { get; set; }
        public int? ParentVersion { get; set; } = null;

        public virtual ICollection<EnumVariant> Variants { get; set; }

        public void CreateReferences()
        {
            Parallel.ForEach(this.Variants, v => v.CreateReferences());
        }
    }
}
