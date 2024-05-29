using UnityEngine;
using UnityEngine.UI;

using Eevee;
using Voltorb;

namespace Iris
{
	internal sealed class AbilityButton : SelectableButton<AbilitySpec>
	{
        [SerializeField]
        private Text m_AbilityName;

        private AbilitySpec m_AbilitySpec;

        private const string kNoValidAbility = "-";

        public override void BindProperties(AbilitySpec props)
        {
            m_AbilitySpec = props;

            m_AbilityName.text = m_AbilitySpec.name.ToUpper();
            interactable = true;
        }

        public override void BindPropertiesToNull()
        {
            m_AbilitySpec = null;

            m_AbilityName.text = kNoValidAbility;
            interactable = false;
        }
    }
}
