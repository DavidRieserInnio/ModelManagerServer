using ModelManagerServer.Models.Exceptions;
using ModelManagerServer.Models.Interfaces;

namespace ModelManagerServer.Models
{
    public readonly struct SubstitutionProviderCollection : ISubstitutionProvider
    {
        private readonly ISubstitutionProvider[] _substitutionProviders;

        public SubstitutionProviderCollection(params ISubstitutionProvider[] substitutionProviders)
        {
            this._substitutionProviders = substitutionProviders;
        }

        public SubstitutionProviderCollection(ICollection<ISubstitutionProvider> substitutionProviders)
        {
            this._substitutionProviders = substitutionProviders.ToArray();
        }

        public bool CanSubstitute(string original)
        {
            return this._substitutionProviders.Any();
        }

        public Result<string, SubstitutionException> GetSubstitution(string original)
        {
            return this._substitutionProviders
                .Select(s => s.GetSubstitution(original))
                .FirstOrDefault(s => s.IsOk, new MissingSubstitutionException(original))
                .Get();
        }
    }
}
