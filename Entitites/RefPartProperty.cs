using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("REF_Part_Property", Schema = "modelmanager")]
    public class RefPartProperty
    {
        [ForeignKey(nameof(Part))]
        public Guid Part_Id { get; set; }
        [ForeignKey(nameof(PartProperty))]
        public Guid PartProperty_Id { get; set; }

        public int PropertyPosition { get; set; }

        public virtual Part Part { get; set; }
        public virtual PartProperty PartProperty { get; set; }
    }
}
