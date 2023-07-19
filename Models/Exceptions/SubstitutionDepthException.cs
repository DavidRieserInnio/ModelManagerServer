using System.Runtime.Serialization;

namespace ModelManagerServer.Models.Exceptions
{
    [Serializable]
    public class SubstitutionDepthException : SubstitutionException
    {
        public SubstitutionDepthException(string expression, uint depth)
            : base($"Reached Maximum Substitution Depth {depth} for Expression {{{expression}}}!") { }

        protected SubstitutionDepthException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
