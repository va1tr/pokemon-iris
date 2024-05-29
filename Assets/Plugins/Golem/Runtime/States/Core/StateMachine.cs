using System;
using System.Collections.Generic;

namespace Golem
{
    public sealed class StateMachine<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private readonly Dictionary<T, IState<T>> m_States = new Dictionary<T, IState<T>>(new FastEnumIntEqualityComparer<T>());

        private T m_CurrentStateID;

        public void Start()
        {
            m_States[m_CurrentStateID].Enter();
        }

        public void Update()
        {
            m_States[m_CurrentStateID].Update();
        }

        public void Stop()
        {
            m_States[m_CurrentStateID].Exit();
        }

        public void ChangeState(T stateToTransitionInto)
        {          
            if (!m_CurrentStateID.Equals(stateToTransitionInto))
            {
                m_States[m_CurrentStateID].Exit();

                SetCurrentStateID(stateToTransitionInto);

                m_States[m_CurrentStateID].Enter();
            }
        }

        public void SetCurrentStateID(T stateToSet)
        {
            m_CurrentStateID = stateToSet;
        }

        public void AddStatesToStateMachine(IState<T>[] states)
        {
            #region Debug
#if UNITY_EDITOR
            VerifyTIsEnum();
            VerifyStatesRepresentAllEntriesOfT(states);
#endif
            #endregion

            foreach (var state in states)
            {
                m_States.Add(state.uniqueId, state);
            }
        }

        #region Debug
#if UNITY_EDITOR
        private void VerifyTIsEnum()
        {
            if (!typeof(T).IsEnum)
            {
                var message = string.Concat($"" +
                    $"StateMachine is trying to initialise with an invalid generic type. " +
                    $"Generic type(T) is not an enum. Type: {typeof(T)}");
                throw new ArgumentException(message);
            }
        }

        private void VerifyStatesRepresentAllEntriesOfT(IState<T>[] states)
        {
            VerifyStatesArentMissing(states);
            VerifyNoStatesAreDuplicates(states);
        }

        private void VerifyStatesArentMissing(IState<T>[] states)
        {
            var missing = GetMissingUniqueIDs(states);

            if (missing.Length > 0)
            {
                var message = string.Concat($"" +
                    $"StateMachine trying initialise with an invalid set of states. " +
                    $"Not enough states passed in. Missing states: ");

                foreach (var entry in missing)
                {
                    message = string.Concat(message, entry.ToString());
                    if (entry.Equals(missing[missing.Length - 1]))
                    {
                        message = string.Concat(message, ", ");
                    }
                }

                throw new ArgumentException(message);
            }
        }

        private T[] GetMissingUniqueIDs(IState<T>[] states)
        {
            List<T> registered = new List<T>();

            foreach (var state in states)
            {
                registered.Add(state.uniqueId);
            }

            var entries = Enum.GetValues(typeof(T));
            List<T> missing = new List<T>();

            for (int i = 0; i < entries.Length; i++)
            {
                var entry = (T)entries.GetValue(i);
                if (!registered.Contains(entry))
                {
                    missing.Add(entry);
                }
            }

            return missing.ToArray();
        }

        private void VerifyNoStatesAreDuplicates(IState<T>[] states)
        {
            var duplicates = GetDuplicateUniqueIDs(states);

            if (duplicates.Length > 0)
            {
                var message = string.Concat($"" +
                    $"StateMachine trying initialise with an invalid set of states. " +
                    $"Duplicate states passed in. Duplicate states: ");

                foreach (var entry in duplicates)
                {
                    message = string.Concat(message, entry.ToString());
                    if (entry.Equals(duplicates[duplicates.Length - 1]))
                    {
                        message = string.Concat(message, ", ");
                    }
                }

                throw new ArgumentException(message);
            }
        }

        private T[] GetDuplicateUniqueIDs(IState<T>[] states)
        {
            List<T> registered = new List<T>();

            foreach (var state in states)
            {
                registered.Add(state.uniqueId);
            }

            var entries = Enum.GetValues(typeof(T));

            for (int i = 0; i < entries.Length; i++)
            {
                var entry = (T)entries.GetValue(i);
                if (registered.Contains(entry))
                {
                    registered.Remove(entry);
                }
            }

            return registered.ToArray();
        }
#endif
        #endregion
    }
}
