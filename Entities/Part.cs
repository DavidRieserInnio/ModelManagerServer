using ModelManagerServer.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entities
{
    [Table("Parts", Schema = "modelmanager")]
    public class Part : ISubstitutable<St4.Part>
    {
        public Guid Id { get; set; }
        public int Version { get; set; }

        public string Name { get; set; }
        public int Type { get; set; }
        public string ElementText { get; set; }

        public Guid? Rule_Id { get; set; }
        public Guid? Enum_Id { get; set; }
        public int? Enum_Version { get; set; }

        public virtual List<Model> Models { get; set; }
        public virtual Rule? Rule { get; set; }
        public virtual Enum? Enum { get; set; }
        public virtual List<PartProperty> PartProperties { get; set; }
        public virtual List<PartPermission> PartPermissions { get; set; }

        public virtual List<RefModelPart> RefModelsParts { get; set; }

        public void CreateReferences()
        {
            if (this.Id == Guid.Empty) 
                this.Id = Guid.NewGuid();

            this.Enum?.CreateReferences();
            this.Rule?.CreateReferences();
            this.PartProperties.ForEach(p => p.CreateReferences());
            this.PartPermissions.ForEach(p => p.CreateReferences());

            for (int i = 0; i < this.PartProperties.Count; i++)
                this.PartProperties[i].PropertyPosition = i;
        }

        public St4.Part Substitute(ISubstitutionProvider provider)
        {
            throw new NotImplementedException();
        }
    }
}
