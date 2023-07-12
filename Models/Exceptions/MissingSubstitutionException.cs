using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace ModelManagerServer.Models.Exceptions
{
    [Serializable]
    public class MissingSubstitutionException : SubstitutionException
    {
        public MissingSubstitutionException(string expression) 
            : base($"Missing Value for Expression {{{expression}}}!") { }

        protected MissingSubstitutionException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
