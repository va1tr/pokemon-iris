using Eevee;

namespace Iris
{
    internal readonly struct PartyGraphicProperties
    {
        internal readonly Pokemon[] collection;

        private PartyGraphicProperties(Pokemon[] collection)
        {
            this.collection = collection;
        }

        internal static PartyGraphicProperties CreateProperties(Pokemon[] collection)
        {
            return new PartyGraphicProperties(collection);
        }
    }
}
