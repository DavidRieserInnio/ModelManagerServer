using ModelManagerServer.St4.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelManagerServer.St4
{
    /// <summary>
    ///     See ST4 Configurator: <see href="https://github.com/innio-etechnik/Configurator_V2/blob/master/Model/Config/Property.cs" />
    /// </summary>
    public class Property
    {
        #region Properties
        // ReSharper disable InconsistentNaming
        public Guid Parts_Id { get; set; }

        public int Parts_Version { get; set; }
        public string Properties_Key { get; set; }

        public Guid Properties_Id { get; set; }

        public int Properties_Position { get; set; }

        public string Properties_Value { get; set; }
        public bool Properties_UseForImport { get; set; }
        public Guid? Properties_GroupCollection { get; set; }

        public Guid? Properties_ComboBoxItemGroup { get; set; }
        public Guid? Localization_TextId { get; set; }

        // public bool Translated { get; set; }
        public St4PropertyTranslationState Properties_TranslationState { get; set; }
        public Dictionary<string, LocalizationText> Properties_TranslationTexts { get; set; }

        private static int s_count;
        // ReSharper enable InconsistentNaming
        #endregion

        #region CTOR

        //default constructor needed for serialization out of DB
        public Property() { }

        public Property(Part part, bool translatable = false)
        {
            Parts_Id = part.Parts_Id;
            Parts_Version = part.Parts_Version;
            Properties_Id = Guid.NewGuid();
            Properties_Key = "key " + s_count.ToString("00000");
            Properties_Position = s_count++;
            Properties_Value = "";
            Properties_TranslationState = translatable ? St4PropertyTranslationState.NotUpToDate : St4PropertyTranslationState.LanguageNeutral;
            Properties_TranslationTexts = new Dictionary<string, LocalizationText>();
            Localization_TextId = translatable ? Guid.NewGuid() : Guid.Empty;
        }

        #endregion

        public void SetLocalizationTexts(Dictionary<Guid, List<LocalizationText>> lt)
        {
            Properties_TranslationTexts = new Dictionary<string, LocalizationText>();
            if (Localization_TextId == null) return;
            if (lt.ContainsKey((Guid)Localization_TextId))
            {
                var thisTexts = lt[(Guid)Localization_TextId].ToDictionary(x => x.Localization_LanguageKey, x => x);
                Properties_TranslationTexts = thisTexts;
            }
        }
        public void UpdatePropertiesValueTranslation(string languageKey)
        {
            if (Properties_TranslationState == St4PropertyTranslationState.LanguageNeutral) return;

            // neuen Wert setzen
            if (!Properties_TranslationTexts.ContainsKey(languageKey))
            {
                // wenn fremdsprache nicht existent >> setze Fallback
                Properties_Value = Properties_TranslationTexts[Common.FallbackLanguage].Localization_Text;
            }
            else
            {
                Properties_Value = Properties_TranslationTexts[languageKey].Localization_Text;
            }
        }

        public void UpsertValue(string languageKey, string value)
        {
            // update text if translated property
            if (Properties_TranslationState != St4PropertyTranslationState.LanguageNeutral)
            {
                if (!Properties_Value.Equals(value)) Properties_TranslationState = St4PropertyTranslationState.NotUpToDate;

                // create entry if not existing yet
                if (!Properties_TranslationTexts.ContainsKey(languageKey))
                {
                    var newValue = new LocalizationText
                    {
                        Localization_TextId = (Guid)Localization_TextId,
                        Localization_LanguageKey = languageKey
                    };
                    Properties_TranslationTexts.Add(languageKey, newValue);
                }

                Properties_TranslationTexts[languageKey].Localization_Text = value;
            }

            Properties_Value = value;
        }


        public override string ToString()
        {
            return $"{Properties_Key}: {Properties_Value}";
        }

    }
}