using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ModelManagerServer.Entities
{
    [Table("Enum_Properties", Schema = "modelmanager")]
    public class EnumProperty
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }
        public Guid EnumVariantId { get; set; }

        [JsonIgnore]
        public Guid Enum_Id { get; set; }
        [JsonIgnore]
        public int Enum_Version { get; set; }

        [JsonIgnore]
        public virtual PartEnum Enum { get; set; }

        public void CreateReferences()
        {
            if (this.Id == Guid.Empty)
                this.Id = Guid.NewGuid();
        }
    }
}
