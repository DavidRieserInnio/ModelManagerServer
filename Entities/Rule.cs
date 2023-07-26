using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ModelManagerServer.Entities
{
    [Table("Rules", Schema = "modelmanager")]
    public class Rule
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Content { get; set; }

        [JsonIgnore]
        public Guid? Model_Id { get; set; }
        [JsonIgnore]
        public int? Model_Version { get; set; }

        [JsonIgnore]
        public virtual List<Part> Parts { get; set; } = new();
        [JsonIgnore]
        public virtual Model? Model { get; set; }

        public void CreateReferences()
        {
            if (this.Id == Guid.Empty)
                this.Id = Guid.NewGuid();
        }
    }
}
