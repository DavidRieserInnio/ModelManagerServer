using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entities
{
    [Table("Part_Properties", Schema = "modelmanager")]
    public class PartProperty
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int PropertyPosition { get; set; }
        public Guid Part_Id { get; set; }
        public int Part_Version { get; set; }

        public Part Part { get; set; }
    }
}
