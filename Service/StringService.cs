using ModelManagerServer.Models;
using ModelManagerServer.Models.Exceptions;
using System.Text;

namespace ModelManagerServer.Service
{
    public static class StringService
    {
        private static readonly Delimiters DEFAULT_DELIMITERS = new('{', '}');
        
        public static Result<string, Exception> ReplaceOccurrences(
            string template, Func<string, string?> resolver
        )
        {
            return ReplaceOccurrences(template, resolver, DEFAULT_DELIMITERS);
        }

        public static Result<string, Exception> ReplaceOccurrences(
            string template, Func<string, string?> resolver, Delimiters delimiters
        )
        {
            var ret = FindExpressionPositions(template, delimiters);
            if (ret.IsError) return ret.GetError();

            var expression_positions = ret.Get();
            if (expression_positions.Count == 0) return template;

            var string_length = 0;
            var replacements = new List<string>(expression_positions.Count);

            for (var i = 0; i < expression_positions.Count; i++)
            {
                var pos = expression_positions[i];
                var prev_pos = i == 0 ? new ExpressionPosition(-1, -1) : expression_positions[i - 1];

                var substring = template.Substring(pos.ExpressionStart, pos.Length);
                var replacement = resolver(substring);
                if (replacement == null)
                    return new MissingSubstitutionException(replacement);
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

        private static Result<List<ExpressionPosition>, InvalidExpressionException> FindExpressionPositions(string template, Delimiters delimiters)
        {
            int start_pos, end_pos = 0;
            List<ExpressionPosition> expression_positions = new();

            while (true)
            {
                start_pos = template.IndexOf(delimiters.StartChar, end_pos + 1);
                if (start_pos == -1) break;

                end_pos = template.IndexOf(delimiters.EndChar, start_pos + 1);
                if (end_pos == -1) return (InvalidExpressionException) new OpenExpressionException(template, start_pos);

                expression_positions.Add((start_pos, end_pos));
            }

            return expression_positions;
        }

        public static Result<List<string>, InvalidExpressionException> FindExpressions(string template, Delimiters delimiters)
        {
            var positions = FindExpressionPositions(template, delimiters);
            if (!positions.IsOk) return positions.GetError();
            return positions.Get().Select(e => template.AsSpan(e.ExpressionStart, e.Length).ToString()).ToList();
        }
    }
}
