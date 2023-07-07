using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("REF_Model_Part", Schema = "modelmanager")]
    public class RefModelPart
    {
        public Guid Model_Id { get; set; }
        public Guid Part_Id { get; set; }

        public int PartPosition { get; set; }

        [ForeignKey(nameof(Model_Id))]
        public virtual Model Model { get; set; }
        [ForeignKey(nameof(Part_Id))]
        public virtual Part Part { get; set; }
    }
}
