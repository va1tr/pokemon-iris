using System;

using Eevee;

namespace Iris
{
    internal sealed class AbilityButtonClickedEventArgs : EventArgs
    {
        internal readonly AbilitySpec abilitySpec;

        internal AbilityButtonClickedEventArgs(AbilitySpec abilitySpec)
        {
            this.abilitySpec = abilitySpec;
        }

        internal static AbilityButtonClickedEventArgs CreateEventArgs(AbilitySpec abilitySpec)
        {
            return new AbilityButtonClickedEventArgs(abilitySpec);
        }
    }
}
