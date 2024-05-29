using System;
using System.Collections;

using Eevee;

namespace Arbok
{
    [Serializable]
    public abstract class ItemSpec
    {
        public readonly ScriptableItem asset;

        public readonly string name;
        public readonly ItemType type;

        public int count;

        public ItemSpec(ScriptableItem asset, int count = 1)
        {
            this.asset = asset;

            name = asset.name;
            type = asset.type;

            this.count = count;
        }

        public virtual void PreItemUse(Combatant target, out SpecResult result)
        {
            result = SpecResult.CreateSpecResult(string.Empty, true);

            if (CanUseItem(ref result))
            {
                count--;
            }
        }

        public abstract IEnumerator UseItem(Combatant target);

        public virtual void PostItemUse(Combatant target, out SpecResult result)
        {
            result = SpecResult.CreateSpecResult(string.Empty, true);
        }

        protected virtual bool CanUseItem(ref SpecResult result)
        {
            return CheckItemCountIsGreatorThanZero();
        }

        private bool CheckItemCountIsGreatorThanZero()
        {
            return count > 0;
        }
    }
}
