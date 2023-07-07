using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("TemplateValue", Schema = "modelmanager")]
    public class TemplateValue
    {
        [Key]
        public Guid TemplateValue_Id { get; set; }
        public string Name { get; set; }
        public bool IsTemplateValue { get; set; }
        public string Value { get; set; }

        public virtual ICollection<Model> Models { get; set; }
    }
}
