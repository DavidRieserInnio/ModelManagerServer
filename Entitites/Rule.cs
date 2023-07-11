using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entitites
{
    [Table("Rules", Schema = "modelmanager")]
    public class Rule
    {
        [Key]
        public Guid Rule_Id { get; set; }
        public string Name { get; set; }
        public string Rule_Content { get; set; }
        public Guid? Model_Id { get; set; }
        public int? Model_Version { get; set; }

        [ForeignKey("Model_Id,Model_Version")]
        public Model? Model { get; set; }
    }
}
