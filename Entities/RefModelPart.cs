using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entities
{
    [Table("REF_Models_Parts", Schema = "modelmanager")]
    public class RefModelPart
    {
        public Guid Model_Id { get; set; }
        public int Model_Version { get; set; }
        public Guid Part_Id { get; set; }
        public int Part_Version { get; set; }

        public int Part_Position { get; set; }

        public virtual Model Model { get; set; }
        public virtual Part Part { get; set; }
    }
}
