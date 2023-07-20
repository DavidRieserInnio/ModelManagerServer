using ModelManagerServer.Entities;
using ModelManagerServer.Models;
using ModelManagerServer.Models.Exceptions;
using ModelManagerServer.Models.Interfaces;
using ModelManagerServer.St4;

namespace ModelManagerServer.Service
{
    public static class ConversionService
    {
        public const uint DEFAULT_MAX_SUBSTITUTION_DEPTH = 50;

        public static List<St4.Part> ConvertModel(
            Model model,
            Dictionary<string, string> userDefinedSubstitutions,
            int part_start_position
        )
        {
            var tmpvls = model.TemplateValues;
            var res = tmpvls.ToSubstitutionProvider(userDefinedSubstitutions);
            // TODO: Create Substitution Provider for Part "Article Codes"

            if (!res.IsOk)
                throw res.GetError();

            var provider = res.Get();

            var parts = model.Parts.Select((p, i) =>
            {
                var partId = Guid.NewGuid();
                var partVersion = 1;
                var part = new St4.Part()
                {
                    Parts_Id = partId,
                    Parts_Version = partVersion,
                    Parts_ParentId = null,
                    Parts_ParentVersion = null,
                    PartPermissions = p.PartPermissions.Select(per => new St4.PartPermission()
                    {
                        Parts_Id = partId,
                        Parts_Version = partVersion,
                        Permission = per.Type,
                        RightGroups_Id = per.RightGroupId
                    }
                    ).ToList(),
                    Parts_Position = part_start_position + i,
                    Parts_State = St4.Enums.St4PartState.Working,
                    Parts_Type = p.Type,
                    Properties = p.PartProperties.Select((prop, j) =>
                    {
                        return new St4.Property()
                        {
                            Parts_Id = partId,
                            Parts_Version = partVersion,
                            Localization_TextId = null,
                            Properties_Id = Guid.NewGuid(),
                            Properties_Key = prop.Name,
                            // TODO: Replace Get() Call
                            Properties_Value = StringService.ReplaceOccurrences(prop.Value, resolver).Get(),
                            Properties_ComboBoxItemGroup = null,
                            Properties_GroupCollection = null,
                            Properties_TranslationState = St4.Enums.St4PropertyTranslationState.NotUpToDate,
                            Properties_TranslationTexts = null!,
                            Properties_UseForImport = false,
                            Properties_Position = j,

                        };
                    }).ToList(),
                };

                if (p.Rule is not null)
                {
                    part.Properties.Add(new St4.Property()
                    {
                        Parts_Id = part.Parts_Id,
                        Parts_Version = part.Parts_Version,
                        Localization_TextId = null,
                        Properties_ComboBoxItemGroup = null,
                        Properties_GroupCollection = null,
                        Properties_Id = Guid.NewGuid(),
                        Properties_Key = Common.PropertyVisibleRule,
                        // TODO: Replace Get() Call
                        Properties_Value = StringService.ReplaceOccurrences(p.Rule.Content, resolver).Get(),
                        Properties_Position = part.Properties.Count,
                        Properties_TranslationState = St4.Enums.St4PropertyTranslationState.NotUpToDate,
                        Properties_TranslationTexts = null!,
                        Properties_UseForImport = false,
                    });
                }
                if (p.Enum is not null)
                {
                    part.Properties.AddRange(p.Enum.Properties.Select((ep, j) => new Property()
                    {
                        Properties_ComboBoxItemGroup = ep.EnumVariantId,
                        Localization_TextId = null,
                        Parts_Id = part.Parts_Id,
                        Parts_Version = part.Parts_Version,
                        Properties_GroupCollection = null!,
                        Properties_Id = Guid.NewGuid(),
                        Properties_Key = ep.Name,
                        // TODO: Replace Get() Call
                        Properties_Value = StringService.ReplaceOccurrences(ep.Value, resolver).Get(),
                        Properties_Position = part.Properties.Count + j,
                        Properties_TranslationState = St4.Enums.St4PropertyTranslationState.NotUpToDate,
                        Properties_TranslationTexts = null!,
                        Properties_UseForImport = false,
                    }
                    ));
                }

                return part;
            }).ToList();

            var appliableTemplateValues = model.TemplateValues.Where(x => x.ApplyToParts).ToArray();
            foreach (var part in parts)
            {
                part.Properties.AddRange(appliableTemplateValues.Select((tv, i) => new Property()
                {
                    Localization_TextId = null,
                    Parts_Id = part.Parts_Id,
                    Parts_Version = part.Parts_Version,
                    Properties_GroupCollection = null!,
                    Properties_ComboBoxItemGroup = null!,
                    Properties_Id = Guid.NewGuid(),
                    Properties_Key = tv.Name,
                    Properties_Value = StringService.ReplaceOccurrences(tv.Value, resolver).Get(),
                    Properties_Position = part.Properties.Count + i,
                    Properties_TranslationState = St4.Enums.St4PropertyTranslationState.NotUpToDate,
                    Properties_TranslationTexts = null!,
                    Properties_UseForImport = false,
                }));
            }

            // TODO: Add Model Rule
            // TODO: Add Group and Circuit

            return parts;

            // TODO: Replace Get() Call
            string resolver(string s) => provider.GetSubstitution(s).Get();
        }

        public static Result<ISubstitutionProvider, SubstitutionException> ToSubstitutionProvider(
            this List<TemplateValue> templateValues,
            Dictionary<string, string> additionalSubstitutions,
            uint max_substitution_depth = DEFAULT_MAX_SUBSTITUTION_DEPTH
        )
        {
            var dict = templateValues.ToDictionary(t => t.Name, t => t.Value);
            // NOTE: User Input overrides pre-defined Template Values.
            foreach (var (k, v) in additionalSubstitutions) dict[k] = v;
            // NOTE: DictionarySubstitutionProvider takes a reference to the Dictionary.
            //       This means changing the Dictionary also changes the DictionarySubstitutionProvider.
            DictionarySubstitutionProvider sub_provider = dict;

            foreach (var tv in dict)
            {
                var (key, value) = (tv.Key, tv.Value);

                var newValue = ResolveValueSubstitutions(value, Lookup, max_substitution_depth);

                if (newValue.IsOk)
                    // Override the Value in Dictionary with the fully substituted one.
                    dict[key] = newValue.Get();
                else
                    return newValue.GetError();
            }

            return sub_provider;

            string Lookup(string s) => sub_provider.GetSubstitution(s).GetOr(null!);

        }

        private static Result<string, SubstitutionException> ResolveValueSubstitutions(
            string value,
            Func<string, string> lookup,
            uint max_substitution_depth = DEFAULT_MAX_SUBSTITUTION_DEPTH
        )
        {
            uint i;
            var tempValue = value;

            for (i = 0; i < max_substitution_depth; i++)
            {
                var rep = StringService.ReplaceOccurrences(tempValue, lookup);
                if (rep.IsOk)
                {
                    var newValue = rep.Get();
                    if (newValue is not null) tempValue = newValue;
                    else break;
                }
                else
                {
                    return rep.GetError();
                }
            }

            if (i >= max_substitution_depth)
                return new SubstitutionDepthException(value, max_substitution_depth);

            return tempValue;
        }
    }
}
