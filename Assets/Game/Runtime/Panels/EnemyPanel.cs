using UnityEngine;

using Slowbro;
using Voltorb;

namespace Iris
{
    internal sealed class EnemyPanel : Panel
    {
        public override System.Collections.IEnumerator ShowAsync()
        {
            gameObject.SetActive(true);

            // 2f.
            yield return rectTransform.Translate(new Vector3(-256f, 0f), Vector3.zero, 2f, Space.World, EasingType.EaseOutSine);
        }
    }
}