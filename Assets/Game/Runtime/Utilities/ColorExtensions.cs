using UnityEngine;

namespace Iris
{
    internal static class ColorExtensions
    {
        internal static Color AlphaMultiplied(this Color color, float multiplier)
        {
            return new Color(color.r, color.g, color.b, color.a * multiplier);
        }
    }
}
