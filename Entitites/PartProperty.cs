using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("Part_Properties", Schema = "modelmanager")]
    public class PartProperty
    {
        [Key]
        public Guid PartProperty_Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int PropertyPosition { get; set; }
        public Guid Part_Id { get; set; }
        public int Part_Version { get; set; }

        [ForeignKey("Part_Id,Part_Version")]
        public Part Part { get; set; }
    }
}
