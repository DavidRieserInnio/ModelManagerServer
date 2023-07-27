using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelManagerServer.St4
{
    /// <summary>
    ///     See ST4 Configurator: <see href="https://github.com/innio-etechnik/Configurator_V2/blob/master/Model/Config/LocalizationText.cs" />
    /// </summary>
    [Table("Localization")]
    public class Localization
    {
        public Guid Localization_TextId { get; set; }
        public string Localization_LanguageKey { get; set; }
        public string Localization_Text { get; set; }
    }
}