using System;

using Eevee;

namespace Iris
{
    internal sealed class PartyPokemonButtonClickedEventArgs : EventArgs
    {
        internal readonly Pokemon pokemon;

        internal PartyPokemonButtonClickedEventArgs(Pokemon pokemon)
        {
            this.pokemon = pokemon;
        }

        internal static PartyPokemonButtonClickedEventArgs CreateEventArgs(Pokemon pokemon)
        {
            return new PartyPokemonButtonClickedEventArgs(pokemon);
        }
    }
}
