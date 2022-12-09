namespace LazySloth.Utilities
{
    using UnityEngine;
    using UnityEngine.UI;

    public static class ScrollRectExtension
    {
        public static void ScrollToTop(this ScrollRect scrollRect)
        {
            scrollRect.normalizedPosition = Vector2.up;
        }
    }
}