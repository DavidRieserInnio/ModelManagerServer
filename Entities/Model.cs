using ModelManagerServer.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entities
{
    [Table("Models", Schema = "modelmanager")]
    public class Model : ISubstitutable<IList<St4.Part>>
    {
        public Guid Id { get; set; }
        public int Version { get; set; }

        public string Name { get; set; }
        public int State { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime CreationDateTime { get; set; }

        public virtual IList<Part> Parts { get; set; }
        public virtual Rule? Rule { get; set; } 
        public virtual IList<TemplateValue> TemplateValues { get; set; }

        public virtual IList<RefModelPart> RefModelsParts { get; set; }

        public void CreateReferences()
        {
             this.RefModelsParts = this.Parts.Select((p, i) => new RefModelPart() {
                 Model_Id = this.Id,
                 Model_Version = this.Version,
                 Part_Id = p.Id,
                 Part_Version = this.Version,
                 Part_Position = i,
            }).ToList();
        }

        public IList<St4.Part> Substitute(ISubstitutionProvider provider)
        {
            // TODO: Create wrapped SubstitutionProvider and add Provider for Model.TemplateValues
            return this.Parts.Select(p => p.Substitute(provider)).ToList();
        }
    }
}
