using ModelManagerServer.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entities
{
    [Table("Enums", Schema = "modelmanager")]
    public class PartEnum : ISubstitutable<IList<string>>
    {
        public Guid Id { get; set; }
        public int Version { get; set; }

        public string Name { get; set; }

        public virtual Part Part { get; set; }
        public virtual List<EnumProperty> Properties { get; set; } = new();

        public void CreateReferences()
        {
            if (this.Id == Guid.Empty)
                this.Id = Guid.NewGuid();

            this.Properties.ForEach(x => x.CreateReferences());
        }

        public IList<string> Substitute(ISubstitutionProvider provider)
        {
            throw new NotImplementedException();
        }
    }
}
