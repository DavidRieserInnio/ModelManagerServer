using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("Enum_Property", Schema = "modelmanager")]
    public class EnumVariantProperty
    {
        [Key]
        public Guid EnumVariantProperty_Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual ICollection<EnumVariant> EnumVariants { get; set; }
        public virtual ICollection<RefEnumVariantProperty> RefEnumVariantProperties { get; set; }
    }
}
