using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ModelManagerServer.Entities
{
    [Table("Template_Values", Schema = "modelmanager")]
    public class TemplateValue
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public bool ApplyToParts { get; set; }
        public string Value { get; set; }

        [JsonIgnore]
        public virtual Guid Model_Id { get; set; }
        [JsonIgnore]
        public virtual int Model_Version { get; set; }

        [JsonIgnore]
        public virtual Model Model { get; set; }
    }
}
