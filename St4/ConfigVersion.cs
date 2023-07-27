using ModelManagerServer.St4.Enums;

namespace ModelManagerServer.St4
{
    public class ConfigVersion
    {
        public Guid ConfigVersions_Id { get; set; }
        public string? ConfigVersions_Context { get; set; }
        public string? ConfigVersions_SubContext { get; set; }
        public int? ConfigVersions_SubContextVersion { get; set; }
        public St4ConfigState ConfigVersions_State { get; set; }
        public Guid? ConfigVersions_ParentId { get; set; }
        public string? ConfigVersions_Comment { get; set; }

        public Guid? ConfigVersions_UpdateBy_Users_Id { get; set; }
        public DateTime? ConfigVersions_UpdateTime { get; set; }
        public Guid? ConfigVersions_CreatedBy_Users_Id { get; set; }
        public DateTime? ConfigVersions_CreationTime { get; set; }

    }
}