using System;

using Eevee;

namespace Iris
{
    internal enum PartySelection
    {
        Summary,
        Switch,
        Return
    }

    internal sealed class PartyOptionsButtonClickedEventArgs : EventArgs
    {
        internal readonly PartySelection selection;

        internal readonly Combatant instigator;
        internal readonly Combatant target;

        internal PartyOptionsButtonClickedEventArgs(PartySelection selection, Combatant instigator, Combatant target)
        {
            this.selection = selection;

            this.instigator = instigator;
            this.target = target;
        }

        internal static PartyOptionsButtonClickedEventArgs CreateEventArgs(PartySelection selection, Combatant instigator, Combatant target)
        {
            return new PartyOptionsButtonClickedEventArgs(selection, instigator, target);
        }
    }
}
