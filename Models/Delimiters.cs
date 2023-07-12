namespace ModelManagerServer.Models
{
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
}
