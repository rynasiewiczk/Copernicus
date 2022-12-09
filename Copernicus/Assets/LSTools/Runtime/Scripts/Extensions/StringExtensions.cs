namespace LazySloth.Utilities
{
    using System;
    using System.Text;

    public static class StringExtensions
    {
        public static string ToSnakeCase(this string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (text.Length < 2)
            {
                return text;
            }

            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(text[0]));
            for (int i = 1; i < text.Length; ++i)
            {
                char c = text[i];
                if (char.IsUpper(c))
                {
                    sb.Append('_');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else if (char.IsDigit(c) && i >= 1 && char.IsDigit(text[i - 1]))
                {
                    sb.Append(c);
                }
                else if (char.IsDigit(c))
                {
                    sb.Append('_');
                    sb.Append(c);
                }
                else if (c != '_')
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}