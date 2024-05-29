using UnityEngine;
using UnityEngine.UI;

using Voltorb;

namespace Iris
{
    internal sealed class LevelUpMenu : Menu<PokemonGraphicProperties>
    {
        [SerializeField]
        private Text m_Health;

        [SerializeField]
        private Text m_Attack;

        [SerializeField]
        private Text m_Defence;

        [SerializeField]
        private Text m_Speed;

        private float m_HealthValue;
        private float m_AttackValue;
        private float m_DefenceValue;
        private float m_SpeedValue;

        public override void SetProperties(PokemonGraphicProperties props)
        {
            var pokemon = props.pokemon;

            float hp = pokemon.health.maxValue;
            float atk = pokemon.attack.value;
            float def = pokemon.defence.value;
            float spd = pokemon.speed.value;

            m_Health.text = string.Concat($"{hp - m_HealthValue}");
            m_Attack.text = string.Concat($"{atk - m_AttackValue}");
            m_Defence.text = string.Concat($"{def - m_DefenceValue}");
            m_Speed.text = string.Concat($"{spd - m_SpeedValue}");

            m_HealthValue = hp;
            m_AttackValue = atk;
            m_DefenceValue = def;
            m_SpeedValue = spd;
        }

    }
}
