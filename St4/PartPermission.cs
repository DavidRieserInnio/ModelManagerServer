using ModelManagerServer.St4.Enums;
using System;

namespace ModelManagerServer.St4
{
    /// <summary>
    ///     See ST4 Configurator: <see href="https://github.com/innio-etechnik/Configurator_V2/blob/master/Model/Config/PartPermission.cs" />
    /// </summary>
    public class PartPermission
    {
        #region Properties DB

        public Guid Parts_Id { get; set; }
        public int Parts_Version { get; set; }
        public Guid RightGroups_Id { get; set; }

        public St4Permission Permission { get; set; }

        #endregion

        #region CTOR
        // Empty CTOR for EF
        public PartPermission() { }

        public PartPermission(Part item, Guid rightGroupId, St4Permission permission)
        {
            this.Parts_Id = item.Parts_Id;
            this.Parts_Version = item.Parts_Version;
            this.RightGroups_Id = rightGroupId;
            this.Permission = permission;
        }

        public PartPermission(Guid parts_Id, int parts_Version, Guid rightGroupId, St4Permission permission)
        {
            this.Parts_Id = parts_Id;
            this.Parts_Version = parts_Version;
            this.RightGroups_Id = rightGroupId;
            this.Permission = permission;
        }
        #endregion
    }
}
