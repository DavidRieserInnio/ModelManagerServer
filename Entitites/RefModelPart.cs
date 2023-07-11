using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("REF_Models_Parts", Schema = "modelmanager")]
    [PrimaryKey(nameof(Model_Id), nameof(Model_Version), nameof(Part_Id), nameof(Part_Version))]
    public class RefModelPart
    {
        public Guid Model_Id { get; set; }
        public int Model_Version { get; set; }
        public Guid Part_Id { get; set; }
        public int Part_Version { get; set; }

        public int PartPosition { get; set; }

        [ForeignKey("Model_Id,Model_Version")]
        public virtual Model Model { get; set; }
        [ForeignKey("Part_Id,Part_Version")]
        public virtual Part Part { get; set; }
    }
}
