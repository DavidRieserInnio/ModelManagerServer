using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entities
{
    [Table("Enums", Schema = "modelmanager")]
    public class Enum
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }

        public virtual Part Part { get; set; }
        public virtual IList<EnumProperty> Properties { get; set; }
    }
}
