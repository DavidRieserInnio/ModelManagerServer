using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.St4
{
    //ReSharper disable InconsistentNaming
    [Table("RightGroups")]
    public class RightGroup
    {
        [Key]
        [Column(Order = 0)]
        public Guid RightGroups_Id { get; set; }

        [Required]
        public string RightGroups_Name { get; set; }

        public string RightGroups_Description { get; set; }
        public bool RightGroups_Default { get; set; }

        public int RightGroups_Members { get; set; }

        public RightGroup()
        {
            RightGroups_Id = Guid.NewGuid();
        }

        public RightGroup(string name, string description, bool isDefault)
        {
            RightGroups_Id = Guid.NewGuid();
            RightGroups_Name = name;
            RightGroups_Description = description;
            RightGroups_Default = isDefault;
        }

        public override string ToString()
        {
            return RightGroups_Name;
        }
    }
}