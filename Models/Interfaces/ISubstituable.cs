namespace ModelManagerServer.Models.Interfaces
{
    public interface ISubstitutable<out T>
    {
        T Substitute(ISubstitutionProvider provider);
        IList<string> GetSubstitutableValues()
        {
            // TODO: Remove default Implementation and implement in the actual Classes
            return new List<string>();
        }
    }
}
