using System.Collections;

using UnityEngine;

using Golem;
using Slowbro;

namespace Iris
{
    [ExecuteInEditMode]
    internal sealed class CameraFade : MonoBehaviour
    {
        [SerializeField]
        private Material m_Material;

        [SerializeField]
        private Texture m_Texture;

        private static Material s_Material;
        private static Texture s_Texture;

        private void Awake()
        {
            s_Material = m_Material;
            s_Texture = m_Texture;
        }

        internal static IEnumerator FadeIn()
        {
            yield return s_Material.Fade(1f, 1f, EasingType.EaseOutSine);
        }

        internal static IEnumerator FadeOut()
        {
            yield return s_Material.Fade(0f, 1f, EasingType.EaseInSine);
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (m_Material != null)
            {
                Graphics.Blit(source, destination, m_Material);
            }
        }
    }
}
