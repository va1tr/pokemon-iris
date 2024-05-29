using UnityEngine;

namespace Eevee
{
    public enum StatisticType
    {
        Health,
        Attack,
        Defence,
        Speed,
        Accuracy,
        Evasion
    }

    public class Statistic
    {
        public float value
        {
            get => m_Value;
            set => m_Value = value;
        }

        public float valueModified
        {
            get => m_Value * stageMultipliers[index];
        }

        public float maxValue
        {
            get => m_MaxValue;
            set => m_MaxValue = value;
        }

        public int stage
        {
            get => m_StageValue;
            set => m_StageValue = Mathf.Clamp(value, -6, 6);
        }

        internal int iv
        {
            get => m_IndividualValue;
            set => m_IndividualValue = Mathf.Clamp(value, 0, 31);
        }

        internal int ev
        {
            get => m_EffortValue;
            set => m_EffortValue = Mathf.Min(value, 252);
        }

        private float m_Value;
        private float m_MaxValue;

        private int m_IndividualValue;
        private int m_EffortValue;

        private int m_StageValue;

        private int index => Mathf.FloorToInt(Mathf.Lerp(0, stageMultipliers.Length - 1, Mathf.InverseLerp(-6, 6, stage)));

        private static float[] stageMultipliers = new float[]
        { 2f/8f, 2f/7f, 2f/6f, 2f/5f, 2f/4f, 2f/3f, 2f/2f, 3f/2f, 4f/2f, 5f/2f, 6f/2f, 7f/2f, 8f/2f };
    }
}
