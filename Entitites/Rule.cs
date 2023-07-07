using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("Rule", Schema = "modelmanager")]
    public class Rule
    {
        [Key]
        public Guid Rule_Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        [Column("Rule")]
        public string RuleContent { get; set; }

        public virtual ICollection<Model> Models { get; set; }
    }
}
