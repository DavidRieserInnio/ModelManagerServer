using System.Text;

namespace ModelManagerServer.Service
{
    public static class StringService
    {
        private static readonly Delimiters DEFAULT_DELIMITERS = new('{', '}');
        
        public static string? ReplaceOccurrences(
            string template, Func<string, string?> resolver
        )
        {
            return ReplaceOccurrences(template, resolver, DEFAULT_DELIMITERS);
        }

        public static string? ReplaceOccurrences(
            string template, Func<string, string?> resolver, Delimiters delimiters
        )
        {
            var expression_positions = FindExpressionPositions(template, delimiters);
            if (expression_positions.Count == 0) return null;

            var string_length = 0;
            var replacements = new List<string>(expression_positions.Count);

            for (var i = 0; i < expression_positions.Count; i++)
            {
                var pos = expression_positions[i];
                var prev_pos = i == 0 ? new ExpressionPosition(-1, -1) : expression_positions[i - 1];

                var substring = template.Substring(pos.ExpressionStart, pos.Length); // Could be done using Span : .AsSpan(pos.ExpressionStart, pos.Length);
                var replacement = resolver(substring) ?? throw InvalidExpressionException.MissingLookupValue(substring);
                replacements.Add(replacement);

                string_length += (pos.StartPosition - prev_pos.EndPosition - 1) + replacement.Length;
            }
            string_length += template.Length - expression_positions.Last().EndPosition - 1;

            var builder = new StringBuilder(string_length);

            for (var i = 0; i < expression_positions.Count; i++)
            {
                var rep = replacements[i];
                var pos = expression_positions[i];
                var prev_pos = i == 0 ? new ExpressionPosition(-1, -1) : expression_positions[i - 1];

                builder.Append(template.AsSpan(prev_pos.EndPosition + 1, pos.StartPosition - prev_pos.EndPosition - 1));
                builder.Append(rep);
            }
            builder.Append(template.AsSpan(expression_positions.Last().EndPosition + 1));

            return builder.ToString();
        }

        private static List<ExpressionPosition> FindExpressionPositions(string template, Delimiters delimiters)
        {
            int start_pos, end_pos = 0;
            List<ExpressionPosition> expression_positions = new();

            while (true)
            {
                start_pos = template.IndexOf(delimiters.StartChar, end_pos + 1);
                if (start_pos == -1) break;

                end_pos = template.IndexOf(delimiters.EndChar, start_pos + 1);
                if (end_pos == -1) throw InvalidExpressionException.OpenExpression(template, start_pos);

                expression_positions.Add((start_pos, end_pos));
            }

            return expression_positions;
        }

        public static List<string> FindExpressions(string template, Delimiters delimiters)
        {
            var positions = FindExpressionPositions(template, delimiters);
            return positions.Select(e => template.AsSpan(e.ExpressionStart, e.Length).ToString()).ToList();
        }

        internal record struct ExpressionPosition(int StartPosition, int EndPosition)
        {
            public static implicit operator (int, int)(ExpressionPosition value)
            {
                return (value.StartPosition, value.EndPosition);
            }

            public static implicit operator ExpressionPosition((int, int) value)
            {
                return new ExpressionPosition(value.Item1, value.Item2);
            }

            public int ExpressionStart { get => this.StartPosition + 1; }
            public int ExpressionEnd { get => this.EndPosition + 1; }


            public int Length { get => this.EndPosition - this.StartPosition - 1; }
            public int TotalLength { get => this.EndPosition - this.StartPosition + 1; }
        }

        public record struct Delimiters(char StartChar, char EndChar)
        {
            public static implicit operator (char, char)(Delimiters value)
            {
                return (value.StartChar, value.EndChar);
            }

            public static implicit operator Delimiters((char, char) value)
            {
                return new Delimiters(value.Item1, value.Item2);
            }
        }

        public class InvalidExpressionException : Exception
        {
            public InvalidExpressionException(string message) : base(message) { }

            public static InvalidExpressionException MissingLookupValue(string expression)
            {
                return new InvalidExpressionException($"Missing Value for Expression {{{expression}}}!");
            }

            public static InvalidExpressionException OpenExpression(string template, int position)
            {
                const string PREFIX = "Found open expression in string \"";

                var pos = position + PREFIX.Length;
                var padding = new StringBuilder(pos).Insert(0, " ", pos - 1).ToString();

                var message = $"{PREFIX}{template}\"\n{padding}^";

                return new InvalidExpressionException(message);
            }
        }
    }
}
