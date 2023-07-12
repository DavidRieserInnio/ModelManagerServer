using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entities
{
    [Table("Template_Values", Schema = "modelmanager")]
    public class TemplateValue
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public bool ApplyToParts { get; set; }
        public string Value { get; set; }

        public Guid Model_Id { get; set; }
        public int Model_Version { get; set; }

        public Model Model { get; set; }
    }
}
