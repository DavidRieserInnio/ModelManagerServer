using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("Enums", Schema = "modelmanager")]
    [PrimaryKey(nameof(Enum_Id), nameof(Version))]
    public class Enum
    {
        public Guid Enum_Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EnumProperty> Properties { get; set; }
    }
}
