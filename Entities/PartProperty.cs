using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ModelManagerServer.Entities
{
    [Table("Part_Properties", Schema = "modelmanager")]
    public class PartProperty
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }

        [JsonIgnore]
        public Guid Part_Id { get; set; }
        [JsonIgnore]
        public int Part_Version { get; set; }
        [JsonIgnore]
        public int PropertyPosition { get; set; }

        [JsonIgnore]
        public Part Part { get; set; }

        public void CreateReferences()
        {
            if (this.Id == Guid.Empty)
                this.Id = Guid.NewGuid();
        }
    }
}
