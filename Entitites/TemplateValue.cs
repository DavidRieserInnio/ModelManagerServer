using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("Template_Values", Schema = "modelmanager")]
    public class TemplateValue
    {
        [Key]
        public Guid TemplateValue_Id { get; set; }
        public string Name { get; set; }
        public bool ApplyToParts { get; set; }
        public string Value { get; set; }
        public Guid Model_Id { get; set; }
        public int Model_Version { get; set; }

        [ForeignKey("Model_Id,Model_Version")]
        public Model Model { get; set; }
    }
}
