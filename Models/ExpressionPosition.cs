namespace ModelManagerServer.Models
{
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

        public int ExpressionStart { get => StartPosition + 1; }
        public int ExpressionEnd { get => EndPosition + 1; }


        public int Length { get => EndPosition - StartPosition - 1; }
        public int TotalLength { get => EndPosition - StartPosition + 1; }
    }
}
