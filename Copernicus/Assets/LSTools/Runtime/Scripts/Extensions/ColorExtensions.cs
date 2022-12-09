namespace LazySloth.Utilities
{
    using UnityEngine;

    public static class ColorExtensions
    {
        public static Color SetAlpha(this Color c, float alpha)
        {
            return new Color(c.r, c.g, c.b, alpha);
        }

        public static Color SetIntensity(this Color c, float intensity)
        {
            float factor = Mathf.Pow(2, intensity);
            return new Color(c.r * factor, c.g * factor, c.b * factor);
        }
        
        public static Color SetValue(this Color color, float value)
        {
            Color.RGBToHSV(color, out var h, out var s, out var v);
            v = value;
            return Color.HSVToRGB(h, s, v);
        }
    }
}