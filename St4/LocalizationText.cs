using System;

namespace ModelManagerServer.St4
{
    /// <summary>
    ///     See ST4 Configurator: <see href="https://github.com/innio-etechnik/Configurator_V2/blob/master/Model/Config/LocalizationText.cs" />
    /// </summary>
    internal class LocalizationText
    {
        public Guid Localization_TextId { get; set; }
        public string Localization_LanguageKey { get; set; }
        public string Localization_Text { get; set; }
    }
}