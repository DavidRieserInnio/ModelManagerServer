using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entities
{
    [Table("Models", Schema = "modelmanager")]
    public class Model
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
                 Part_Position = i
            }).ToList();
        }
    }
}
