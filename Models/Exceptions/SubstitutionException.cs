using System.Runtime.Serialization;

namespace ModelManagerServer.Models.Exceptions
{
    [Serializable]
    public class SubstitutionException : FormatException
    {
        public SubstitutionException(string message) : base(message) { }

        protected SubstitutionException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
