using System.Runtime.Serialization;

namespace ModelManagerServer.Models.Exceptions
{
    [Serializable]
    public class InvalidExpressionException : Exception
    {
        public InvalidExpressionException(string message) : base(message) { }

        protected InvalidExpressionException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
