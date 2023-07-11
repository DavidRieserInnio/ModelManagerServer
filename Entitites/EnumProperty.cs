using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("Enum_Properties", Schema = "modelmanager")]
    public class EnumProperty
    {
        [Key]
        public Guid EnumProperty_Id { get; set; }
        public Guid Enum_Id { get; set; }
        public int Enum_Version { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        [ForeignKey("Enum_Id,Enum_Version")]
        public virtual Enum Enum { get; set; }
    }
}
