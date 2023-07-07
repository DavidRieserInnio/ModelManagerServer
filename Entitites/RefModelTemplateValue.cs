using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("REF_Model_TemplateValue", Schema = "modelmanager")]
    public class RefModelTemplateValue
    {
        [ForeignKey(nameof(Model))]
        public Guid Model_Id { get; set; }
        [ForeignKey(nameof(TemplateValue))]
        public Guid Template_Value_Id { get; set; }

        public int Template_Value_Position { get; set; }

        public virtual Model Model { get; set; }
        public virtual TemplateValue TemplateValue { get; set; }
    }
}
