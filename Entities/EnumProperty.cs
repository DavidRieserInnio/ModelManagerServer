using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entities
{
    [Table("Enum_Properties", Schema = "modelmanager")]
    public class EnumProperty
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }
        public Guid EnumVariantId { get; set; }

        public Guid Enum_Id { get; set; }
        public int Enum_Version { get; set; }

        public virtual Enum Enum { get; set; }

        public void CreateReferences()
        {
            if (this.Id == Guid.Empty)
                this.Id = Guid.NewGuid();
        }
    }
}
