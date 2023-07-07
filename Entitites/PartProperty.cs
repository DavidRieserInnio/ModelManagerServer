using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("Part_Property", Schema = "modelmanager")]
    public class PartProperty
    {
        [Key]
        public Guid PartProperty_Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual ICollection<Part> Parts { get; set; }
        [ForeignKey(nameof(PartProperty_Id))]
        public virtual ICollection<RefPartProperty> RefPartProperties { get; set; }
    }
}
