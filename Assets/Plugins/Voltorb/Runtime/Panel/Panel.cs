using UnityEngine;

namespace Voltorb
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class Panel : Menu
    {
        public RectTransform rectTransform
        {
            get
            {
                if (m_RectTransform == null)
                {
                    m_RectTransform = GetComponent<RectTransform>();
                }

                return m_RectTransform;
            }
        }

        private RectTransform m_RectTransform;
    }

    [RequireComponent(typeof(RectTransform))]
    public abstract class Panel<T> : Menu<T> where T : struct
    {
        public RectTransform rectTransform
        {
            get
            {
                if (m_RectTransform == null)
                {
                    m_RectTransform = GetComponent<RectTransform>();
                }

                return m_RectTransform;
            }
        }

        private RectTransform m_RectTransform;
    }
}
