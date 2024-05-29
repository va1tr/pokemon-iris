using UnityEngine;
using UnityEngine.UI;

namespace Slowbro
{
    public static class TweenExtensions
    {
        public static Tween<Material, float> Alpha(this Material material, float end, float duration, EasingType easing)
        {
            return (Tween<Material, float>)new Tween<Material, float>((o) => o.GetFloat("_Alpha"), (o, v) => o.SetFloat("_Alpha", v))
                .Initialise(material).SetStart(material.GetFloat("_Alpha")).SetEnd(end).SetDuration(duration).SetInterpolation(new FloatInterpolator(easing)).Run();
        }

        public static Tween<Material, float> Flash(this Material material, float end, float duration, EasingType easing)
        {
            return (Tween<Material, float>)new Tween<Material, float>((o) => o.GetFloat("_Alpha"), (o, v) => o.SetFloat("_Alpha", v))
                .Initialise(material).SetStart(material.GetFloat("_Alpha")).SetEnd(end).SetDuration(duration).SetInterpolation(new FloatInterpolatorClamped(easing)).Run();
        }

        public static Tween<Material, float> Overlay(this Material material, Texture texture, float blend, float offset, float duration, EasingType easing)
        {
            return (Tween<Material, float>)new Tween<Material, float>((o) => o.GetFloat("_Blend"), (o, v) => { o.SetFloat("_Blend", v); o.SetFloat("_Offset", offset); o.SetTexture("_OverlayTex", texture); })
                .Initialise(material).SetStart(material.GetFloat("_Blend")).SetEnd(blend).SetDuration(duration).SetInterpolation(new FloatInterpolator(easing)).Run();
        }

        public static Tween<Material, float> Fade(this Material material, float end, float duration, EasingType easing)
        {
            return (Tween<Material, float>)new Tween<Material, float>((o) => o.GetFloat("_Alpha"), (o, v) => { o.SetFloat("_Alpha", v); o.SetFloat("_Blend", 1f); })
                .Initialise(material).SetStart(material.GetFloat("_Alpha")).SetEnd(end).SetDuration(duration).SetInterpolation(new FloatInterpolator(easing)).Run();
        }

        public static Tween<Material, float> Blend(this Material material, Texture texture, float end, float duration, EasingType easing)
        {
            return (Tween<Material, float>)new Tween<Material, float>((o) => o.GetFloat("_Blend"), (o, v) => { o.SetFloat("_Blend", v); o.SetFloat("_Alpha", v); })
                .Initialise(material).SetStart(material.GetFloat("_Blend")).SetEnd(end).SetDuration(duration).SetInterpolation(new FloatInterpolator(easing)).Run();
        }

        public static Tween<Slider, float> Interpolate(this Slider slider, float end, float duration, EasingType easing)
        {
            return (Tween<Slider, float>)new Tween<Slider, float>((o) => o.value, (o, v) => o.value = v)
                .Initialise(slider).SetStart(slider.value).SetEnd(end).SetDuration(duration).SetInterpolation(new FloatInterpolator(easing)).Run();
        }

        public static Animation Animate(this Image image, Sprite[] sprites, float frameRate)
        {
            return (Animation)new Animation((o) => o.sprite, (o, v) => o.sprite = v)
                .SetSprites(sprites).SetFrameRate(frameRate).SetNumberOfLoops(1).Initialise(image).Run();
        }

        public static Animation Animate(this Image image, Sprite[] sprites, float frameRate, int numberOfLoops)
        {
            return (Animation)new Animation((o) => o.sprite, (o, v) => o.sprite = v)
                .SetSprites(sprites).SetFrameRate(frameRate).SetNumberOfLoops(numberOfLoops).Initialise(image).Run();
        }

        public static AnchoredPosition Translate(this RectTransform transform, Vector3 end, float duration, Space relativeTo, EasingType easing)
        {
            return (AnchoredPosition)new AnchoredPosition((o) => o.anchoredPosition, (o, v) => o.anchoredPosition = v)
                .Initialise(transform).SetStart(transform.position).SetEnd(end).SetDuration(duration).SetSpace(relativeTo).SetInterpolation(new Vector3InterpolatorClamped(easing)).Run();
        }

        public static AnchoredPosition Translate(this RectTransform transform, Vector3 start, Vector3 end, float duration, Space relativeTo, EasingType easing)
        {
            return (AnchoredPosition)new AnchoredPosition((o) => o.anchoredPosition, (o, v) => o.anchoredPosition = v)
                .Initialise(transform).SetStart(start).SetEnd(end).SetDuration(duration).SetSpace(relativeTo).SetInterpolation(new Vector3InterpolatorClamped(easing)).Run();
        }

        public static AnchoredPosition Scale(this RectTransform transform, Vector3 start, Vector3 end, float duration, Space relativeTo, EasingType easing)
        {
            return (AnchoredPosition)new AnchoredPosition((o) => o.localScale, (o, v) => o.localScale = v)
                .Initialise(transform).SetStart(start).SetEnd(end).SetDuration(duration).SetSpace(relativeTo).SetInterpolation(new Vector3Interpolator(easing)).Run();
        }
    }
}