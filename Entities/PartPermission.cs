﻿using ModelManagerServer.St4.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ModelManagerServer.Entities
{
    [Table("Part_Permissions", Schema = "modelmanager")]
    public class PartPermission
    {
        public Guid Id { get; set; }

        public St4Permission Type { get; set; }
        public Guid RightGroupId { get; set; }

        [JsonIgnore]
        public Guid Part_Id { get; set; }
        [JsonIgnore]
        public int Part_Version { get; set; }

        [JsonIgnore]
        public Part Part { get; set; }

        public void CreateReferences()
        {
            if (this.Id == Guid.Empty)
                this.Id = Guid.NewGuid();
        }
    }
}
