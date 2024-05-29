using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

namespace Voltorb
{
    [RequireComponent(typeof(Text))]
    public sealed class Typewriter : MonoBehaviour
    {
        private Text m_TextComponent;

        internal Text textComponent
        {
            get
            {
                if (m_TextComponent == null)
                {
                    m_TextComponent = GetComponent<Text>();
                }

                return m_TextComponent;
            }
        }

        private WaitForSeconds m_DelayBetweenChars = new WaitForSeconds(kDelayBetweenCharsInSeconds);
        private WaitForSeconds m_DelayBetweenPunctuation = new WaitForSeconds(kDelayBetweenPunctuationInSeconds);

        private readonly static char[] s_PunctutationCharacters = new char[] { '.', ',', '!', '?' };

        private const float kDelayBetweenCharsInSeconds = 0.02f;
        private const float kDelayBetweenPunctuationInSeconds = 0.08f;

        public IEnumerator TypeTextCharByChar(string text)
        {
            int count = 0;
            int length = text.Length;

            while (count < length) 
            {
                count++;

                textComponent.text = text.Insert(count, "<color=#FFFFFF00>") + "</color>";

                yield return Array.Exists(s_PunctutationCharacters, (x) => x == text[count - 1]) ?
                   m_DelayBetweenPunctuation : m_DelayBetweenChars;
            }
        }

        public void PrintTextCharByChar(string text)
        {
            Cleanup();

            StartCoroutine(TypeTextCharByChar(text));
        }

        public void PrintCompletedText(string text)
        {
            Cleanup();

            textComponent.text = text;
        }

        public void CleanupAndClearAllText()
        {
            StopAllCoroutines();

            textComponent.text = string.Empty;
        }

        private void Cleanup()
        {
            StopCoroutine(TypeTextCharByChar(string.Empty));
        }
    }
}
