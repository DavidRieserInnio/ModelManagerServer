using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("Models", Schema = "modelmanager")]
    [PrimaryKey(nameof(Model_Id), nameof(Version))]
    public class Model
    {
        public Guid Model_Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime CreationDateTime { get; set; }

        public virtual ICollection<Part> Parts { get; set; }
        public virtual Rule? Rule { get; set; } 
        public virtual ICollection<TemplateValue> TemplateValues { get; set; }

        public virtual ICollection<RefModelPart> RefModelParts { get; set; }

        public void CreateReferences()
        {
            this.RefModelParts = this.Parts.Select((p, i) => new RefModelPart() {
                Model = this,
                Part = p,
                PartPosition = i,
            }).ToList();

        }
    }
}
