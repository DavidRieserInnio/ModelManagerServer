using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.St4
{
    [Table("REF_ConfigVersions_Parts")]
    public class RefModelPart
    {
        public Guid ConfigVersions_Id { get; set; }
        public Guid Parts_Id { get; set; }
        public int Parts_Version { get; set; }
        public int Parts_Position { get; set; }
    }
}
