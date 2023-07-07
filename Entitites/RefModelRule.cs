using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("REF_Model_Rule", Schema = "modelmanager")]
    public class RefModelRule
    {
        [ForeignKey(nameof(Model))]
        public Guid Model_Id { get; set; }
        [ForeignKey(nameof(Rule))]
        public Guid Rule_Id { get; set; }

        public virtual Model Model { get; set; }
        public virtual Rule Rule { get; set; }
    }
}
