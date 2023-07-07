using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModelManagerServer.St4.Enums;

namespace ModelManagerServer.St4
{
    /// <summary>
    ///     See <see href="https://github.com/innio-etechnik/Configurator_V2/blob/master/Model/Config/ConfigVersion.cs">St4 Configurator</see>
    /// </summary>
    //ReSharper disable InconsistentNaming
    internal class ConfigVersion
    {
        [Key]
        [Column(Order = 0)]
        public Guid ConfigVersions_Id { get; set; }   //ElementValuesId

        [Required]
        [MaxLength(16)]
        public string ConfigVersions_Context { get; set; }

        [Required]
        [MaxLength(16)]
        public string ConfigVersions_SubContext { get; set; }

        [Required]
        public int ConfigVersions_SubContextVersion { get; set; }

        [Required]
        public St4ConfigState ConfigVersions_State { get; set; }

        public Guid? ConfigVersions_ParentId { get; set; }

        public string ConfigVersions_Comment { get; set; }

        [NotMapped]
        [XmlIgnore]
        public string ShortDescription
        {
            //get => String.Format("{0} - v.{1} [{2}] ", SubContext, SubContextVersion, State);
            // get => String.Format("{0} - {1}", ConfigVersions_Context, ConfigVersions_SubContext);
            get => $"v{ConfigVersions_SubContextVersion} [{ConfigVersions_State}]";
        }

        [NotMapped]
        [XmlIgnore]
        public string MediumDescription
        {
            //get => String.Format("{0} - v.{1} [{2}] ", SubContext, SubContextVersion, State);
            get => $"{ConfigVersions_Context} [{ConfigVersions_SubContext}] v{ConfigVersions_SubContextVersion}";
        }

        [NotMapped]
        [XmlIgnore]
        public string LongDescription
        {
            //get => String.Format("{0} - v.{1} [{2}] ", SubContext, SubContextVersion, State);
            get => $"{ConfigVersions_Context} - {ConfigVersions_SubContext} v{ConfigVersions_SubContextVersion} [{ConfigVersions_State}]";
        }
    }
}