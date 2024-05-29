using Eevee;
using Voltorb;

namespace Iris
{
    internal readonly struct PokemonGraphicProperties
    {
        internal readonly Pokemon pokemon;

        private PokemonGraphicProperties(Pokemon pokemon)
        {
            this.pokemon = pokemon;
        }

        internal static PokemonGraphicProperties CreateProperties(Pokemon pokemon)
        {
            return new PokemonGraphicProperties(pokemon);
        }
    }
}
