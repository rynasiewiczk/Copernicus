namespace LazySloth.Utilities
{
    using UnityEngine;

    public static class GradientHelper
    {
        public static Gradient CreateSingleColor(Color color)
        {
            Gradient g = new Gradient();
            GradientColorKey[] gck = new GradientColorKey[1];
            GradientAlphaKey[] gak = new GradientAlphaKey[1];
            gck[0].color = color;
            gck[0].time = 0f;
            gak[0].alpha = color.a;
            gak[0].time = 0f;
            g.SetKeys(gck, gak);

            return g;
        }
    }
}