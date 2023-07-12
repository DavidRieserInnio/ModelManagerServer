using ModelManagerServer.Models.Exceptions;
using ModelManagerServer.Models.Interfaces;
using ModelManagerServer.Service;

namespace ModelManagerServer.Models
{
    public class DictionarySubstitutionProvider : ISubstitutionProvider
    {
        private readonly Dictionary<string, string> _substitutions;

        public DictionarySubstitutionProvider(Dictionary<string, string> substitutions)
        {
            this._substitutions = substitutions;
        }

        public DictionarySubstitutionProvider(ICollection<KeyValuePair<string, string>> substitutions)
        {
            this._substitutions = substitutions.ToDictionary();
        }

        public Result<string, SubstitutionException> GetSubstitution(string original)
        {
            return this._substitutions.TryGetValue(original, out var result)
                ? result
                : new MissingSubstitutionException(original);
        }
    }
}
