using UnityEngine;

namespace Eevee
{
    public abstract class Summoner : MonoBehaviour
    {
        protected virtual void Start()
        {
            CreateStartupPokemonParty();
        }

        protected abstract void CreateStartupPokemonParty();

        public abstract Pokemon GetActiveOrFirstPokemonThatIsNotFainted();

        public abstract Pokemon GetFirstPokemonThatIsNotFainted();
    }
}
