namespace LazySloth.Utilities
{
    using UnityEngine;

    public static class TextMeshProExtensions
    {
        public static string TMP_SetColor(this string text, Color color)
        {
            var hexColor = ColorUtility.ToHtmlStringRGB(color);
            return $"<#{hexColor}>{text}</color>";
        }
        
        public static string TMP_SetBold(this string text)
        {
            return $"<b>{text}</b>";
        }

        public static string TMP_InsertIconAtEnd(this string text, string iconName, bool spaceBetween = false)
        {
            var iconText = $"<sprite name={iconName}>";
            var between = spaceBetween ? " " : string.Empty;
            return $"{text}{between}{iconText}";
        }
        
        public static string TMP_InsertIconAtFront(this string text, string iconName, bool spaceBetween = false)
        {
            var iconText = $"<sprite name={iconName}>";
            var between = spaceBetween ? " " : string.Empty;
            return $"{iconText}{between}{text}";
        }

        public static string TMP_InsertLineBreak(this string text)
        {
            return $"{text}\n";
        }
    }
}