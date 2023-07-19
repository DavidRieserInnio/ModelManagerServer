using ModelManagerServer.Entities;
using ModelManagerServer.Models;
using ModelManagerServer.Models.Exceptions;
using ModelManagerServer.Models.Interfaces;

namespace ModelManagerServer.Service
{
    public static class ConversionService
    {
        public const uint MAXIMUM_SUBSTITUTION_DEPTH = 50;

        public static List<Part> ConvertModel(
            Model model,
            ISubstitutionProvider userDefinedSubstitutions
        )
        {
            var tmpvls = model.TemplateValues;
            var provider = tmpvls.ToSubstitutionProvider(userDefinedSubstitutions);


            return new();
        }

        public static ISubstitutionProvider ToSubstitutionProvider(
            this List<TemplateValue> templateValues,
            ISubstitutionProvider additionalSubstitutions,
            uint max_substitution_depth = MAXIMUM_SUBSTITUTION_DEPTH
        )
        {
            var dict = templateValues.ToDictionary(t => t.Name, t => t.Value);
            // NOTE: DictionarySubstitutionProvider takes a reference to the Dictionary.
            //       This means changing the Dictionary also changes the DictionarySubstitutionProvider.
            DictionarySubstitutionProvider tv_provider = dict;
            var sub_provider = new SubstitutionProviderCollection(tv_provider, additionalSubstitutions);

            foreach (var tv in templateValues)
            {
                uint i;
                var (key, value) = (tv.Name, tv.Value);
                var tempValue = value;

                for (i = 0; i < max_substitution_depth; i++)
                {
                    var rep = StringService.ReplaceOccurrences(tempValue, lookup);
                    if (rep.IsOk)
                    {
                        var newValue = rep.Get();
                        if (newValue is not null)
                        {
                            tempValue = newValue;
                        }
                        else break;
                    } 
                    else
                    {
                        // TODO: Return Result?
                        throw rep.GetError();
                    }
                }

                if (i >= max_substitution_depth)
                    // TODO: Return Result?
                    throw new SubstitutionDepthException(value, max_substitution_depth);

                // Override the Value in Dictionary with the fully substituted one.
                dict[key] = tempValue;
            }

            return null!;
        
            string lookup(string s) => sub_provider.GetSubstitution(s).GetOr(null!);
        }
    }
}
