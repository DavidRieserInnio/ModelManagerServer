using ModelManagerServer.St4.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.St4
{
    //ReSharper disable InconsistentNaming
    public class RightGroup
    {

        public Guid RightGroups_Id { get; set; }
        public Guid? RightGroups_ParentId { get; set; }

        public string RightGroups_Name { get; set; }

        public string RightGroups_Description { get; set; }
        public bool RightGroups_Default { get; set; }
        public CompanyRelation RightGroups_Relation { get; set; }

        [NotMapped] public int RightGroups_Members { get; set; }
        [NotMapped] public int Index { get; set; }



        public RightGroup()
        {
        }
        public RightGroup(string name, string description, bool isDefault)
        {
            RightGroups_Id = Guid.NewGuid();
            RightGroups_Name = name;
            RightGroups_Description = description;
            RightGroups_Default = isDefault;
            RightGroups_Relation = CompanyRelation.Internal;
        }
    }
}