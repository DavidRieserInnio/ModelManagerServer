using ModelManagerServer.Models.Exceptions;

namespace ModelManagerServer.Models.Interfaces
{
    public interface ISubstitutionProvider
    {
        public Result<string, SubstitutionException> GetSubstitution(string original);
        public bool CanSubstitute(string original)
        {
            return this.GetSubstitution(original).IsOk;
        }
    }
}