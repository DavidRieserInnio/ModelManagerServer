using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("REF_Enum_Variant_Property", Schema = "modelmanager")]
    public class RefEnumVariantProperty
    {
        [ForeignKey(nameof(EnumVariant))]
        public Guid EnumVariant_Id { get; set; }
        [ForeignKey(nameof(EnumVariantProperty))]
        public Guid EnumProperty_Id { get; set; }
        public int EnumVariantPosition { get; set; }

        public virtual EnumVariant EnumVariant { get; set; }
        public virtual EnumVariantProperty EnumVariantProperty { get; set; }
    }
}
