using UnityEngine;

namespace Slowbro
{
    public enum EasingType
    {
        linear,
        EaseOutSine,
        EaseInSine,
        PingPong
    }

    public readonly struct Easing
    {
        internal static float Resolve(EasingType type, float time)
        {
            switch (type)
            {
                case EasingType.linear:
                    return Linear(time);

                case EasingType.EaseOutSine:
                    return EaseOutSine(time);

                case EasingType.EaseInSine:
                    return EaseInSine(time);

                case EasingType.PingPong:
                    return PingPong(time);

                default:
                    break;
            }

            return time;
        }

        private static float Linear(float time)
        {
            return time;
        }

        private static float EaseOutSine(float time)
        {
            return Mathf.Sin(time * Mathf.PI / 2f);
        }

        private static float EaseInSine(float time)
        {
            return 1f - Mathf.Cos(time * Mathf.PI / 2f);
        }

        private static float PingPong(float time)
        {
            return Mathf.PingPong(time * 2f, 1f);
        }
    }
}