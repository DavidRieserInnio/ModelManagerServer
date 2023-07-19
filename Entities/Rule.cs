using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entities
{
    [Table("Rules", Schema = "modelmanager")]
    public class Rule
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Content { get; set; }

        public Guid? Model_Id { get; set; }
        public int? Model_Version { get; set; }

        public virtual IList<Part> Parts { get; set; }
        public virtual Model? Model { get; set; }

        public void CreateReferences()
        {
            if (this.Id == Guid.Empty)
                this.Id = Guid.NewGuid();
        }
    }
}
