using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("Model", Schema = "modelmanager")]
    public class Model
    {
        [Key]
        public Guid Model_Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public int Type { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime CreationDateTime { get; set; }

        public virtual ICollection<Rule> Rules { get; set; }
        public virtual ICollection<TemplateValue> TemplateValues { get; set; }
        public virtual ICollection<Part> Parts { get; set; }

        public virtual ICollection<RefModelPart> RefModelParts { get; set; }
        public virtual ICollection<RefModelTemplateValue> RefModelTemplateValues { get; set; }
        public virtual ICollection<RefModelRule> RefModelRules { get; set; }

        public void CreateReferences()
        {
            Parallel.ForEach(this.Parts, p => p.CreateReferences());

            this.RefModelParts = this.Parts.Select((p, i) => new RefModelPart() {
                Model = this,
                Part = p,
                PartPosition = i,
            }).ToList();

            this.RefModelTemplateValues = this.TemplateValues.Select((t, i) => new RefModelTemplateValue()
            {
                Model = this,
                TemplateValue = t,
                Template_Value_Position = i,
            }).ToList();

            this.RefModelRules = this.Rules.Select((t, i) => new RefModelRule()
            {
                Model = this,
                Rule = t,
            }).ToList();
        }
    }
}
