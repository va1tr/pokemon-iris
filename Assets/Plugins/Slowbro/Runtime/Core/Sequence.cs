using System.Collections;

using UnityEngine;

namespace Slowbro
{
    public class Sequence : Routine
    {
        private bool m_IsDone = false;

        public Sequence(MonoBehaviour monoBehaviour, params IEnumerator[] coroutines)
        {
            monoBehaviour.StartCoroutine(Wrapper(coroutines));
        }

        public override void Initialise()
        {
            
        }

        public override bool Update()
        {
            return IsCompleted();
        }

        public override bool IsCompleted()
        {
            return !m_IsDone;
        }

        private IEnumerator Wrapper(IEnumerator[] coroutines)
        {
            for (int i = 0; i < coroutines.Length; i++)
            {
                yield return coroutines[i];
            }

            m_IsDone = true;
        }
    }

    public class Parallel : Routine
    {
        private bool[] m_IsDone;

        public Parallel(MonoBehaviour monoBehaviour, params IEnumerator[] coroutines)
        {
            m_IsDone = new bool[coroutines.Length];

            for (int i = 0; i < coroutines.Length; i++)
            {
                monoBehaviour.StartCoroutine(Wrapper(coroutines[i], i));
            }
        }

        public override void Initialise()
        {
            
        }

        public override bool Update()
        {
            return IsCompleted();
        }

        public override bool IsCompleted()
        {
            for (int i = 0; i < m_IsDone.Length; i++)
            {
                if (!m_IsDone[i])
                {
                    return true;
                }
            }

            return false;
        }

        private IEnumerator Wrapper(IEnumerator coroutine, int index)
        {
            m_IsDone[index] = false;

            yield return coroutine;

            m_IsDone[index] = true;
        }
    }
}