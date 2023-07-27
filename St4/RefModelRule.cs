using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.St4
{
    [Table("REF_ConfigVersions_RuleMethods")]
    public class RefModelRule
    {
        public Guid ConfigVersions_Id { get; set; }
        public Guid RuleMethods_Id { get; set; }
        public int RuleMethods_Version { get; set; }
        public int RuleMethods_Position { get; set; }
    }
}
