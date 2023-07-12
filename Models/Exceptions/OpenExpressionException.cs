using System.Runtime.Serialization;
using System.Text;

namespace ModelManagerServer.Models.Exceptions
{
    [Serializable]
    public class OpenExpressionException : InvalidExpressionException
    {
        public OpenExpressionException(string message) : base(message) { }

        public OpenExpressionException(string template, int position)
            : base(CreateMessage(template, position)) { }

        private static string CreateMessage(string template, int position)
        {
            const string PREFIX = "Found open expression in string \"";

            var pos = position + PREFIX.Length;
            var padding = new StringBuilder(pos).Insert(0, " ", pos - 1).ToString();

            return $"{PREFIX}{template}\"\n{padding}^";
        }

        protected OpenExpressionException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
