using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("Enum_Variant", Schema = "modelmanager")]
    public class EnumVariant
    {
        [Key]
        public Guid EnumVariant_Id { get; set; }
        [ForeignKey(nameof(Enum))]
        public Guid Enum_Id { get; set; }
        public string ItemText { get; set; }
        public string ArticleCode { get; set; }

        public virtual Enum Enum { get; set; }
        public virtual ICollection<EnumVariantProperty> EnumVariantProperties { get; set; }
        public virtual ICollection<RefEnumVariantProperty> RefEnumVariantProperties { get; set; }

        public void CreateReferences()
        {
            this.RefEnumVariantProperties = this.EnumVariantProperties.Select((v, i) => new RefEnumVariantProperty()
            {
                EnumVariant = this,
                EnumVariantProperty = v,
                EnumVariantPosition = i,
            }).ToList();
        }
    }
}
