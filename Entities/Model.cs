﻿using ModelManagerServer.Models.Interfaces;
using ModelManagerServer.St4.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.Entities
{
    [Table("Models", Schema = "modelmanager")]
    public class Model : ISubstitutable<IList<St4.Part>>
    {
        public Guid Id { get; set; }
        public int Version { get; set; }

        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public St4PartState State { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime CreationDateTime { get; set; }

        public virtual List<Part> Parts { get; set; } = new();
        public virtual Rule? Rule { get; set; } = null;
        public virtual List<TemplateValue> TemplateValues { get; set; } = new();

        [JsonIgnore]
        public virtual List<RefModelPart> RefModelsParts { get; set; } = new();

        public void CreateReferences(Guid userId)
        {
            if (this.Id == Guid.Empty) this.Id = Guid.NewGuid();
            else this.Version++;

            this.CreatedByUserId = userId;
            this.CreationDateTime = DateTime.Now;

            foreach (var part in this.Parts)
                part.CreateReferences();

            this.RefModelsParts = this.Parts.Select((p, i) => new RefModelPart()
            {
                Model_Id = this.Id,
                Model_Version = this.Version,
                Part_Id = p.Id,
                Part_Version = this.Version,
                Part_Position = i,
            }).ToList();
        }

        public IList<St4.Part> Substitute(ISubstitutionProvider provider)
        {
            // TODO: Create wrapped SubstitutionProvider and add Provider for Model.TemplateValues
            return this.Parts.Select(p => p.Substitute(provider)).ToList();
        }
    }
}
