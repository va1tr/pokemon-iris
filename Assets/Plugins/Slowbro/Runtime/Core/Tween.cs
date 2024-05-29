using System;

using UnityEngine;
using UnityEngine.UI;

namespace Slowbro
{
    public abstract class Routine : CustomYieldInstruction
    {
        public override bool keepWaiting => Update();

        public abstract void Initialise();

        public abstract bool Update();
        public abstract bool IsCompleted();

        public Routine Run()
        {
            Initialise();
            return this;
        }
    }

    public class Tween<T1, T2> : Routine
    {
        internal readonly Func<T1, T2> getter;
        internal readonly Action<T1, T2> setter;

        protected Space m_RelativeTo;

        protected T1 m_Component;

        protected IInterpolator<T2> m_Interpolator;

        protected T2 m_StartValue;
        protected T2 m_EndValue;

        protected float m_TimeElapsed;
        protected float m_Duration;

        internal Tween(Func<T1, T2> getter, Action<T1, T2> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }

        public override void Initialise()
        {
            m_TimeElapsed = 0f;
        }

        public override bool Update()
        {
            m_TimeElapsed += Time.deltaTime;

            setter(m_Component, m_Interpolator.Interpolate(m_StartValue, m_EndValue, Mathf.Min(m_TimeElapsed / m_Duration, 1.0f)));

            return IsCompleted();
        }

        public override bool IsCompleted()
        {
            return m_TimeElapsed < m_Duration;
        }

        internal virtual Tween<T1, T2> Initialise(T1 component)
        {
            m_Component = component;
            return this;
        }

        internal virtual Tween<T1, T2> SetStart(T2 start)
        {
            m_StartValue = start;
            return this;
        }

        internal virtual Tween<T1, T2> SetEnd(T2 end)
        {
            m_EndValue = end;
            return this;
        }

        internal virtual Tween<T1, T2> SetDuration(float duration)
        {
            m_Duration = duration;
            return this;
        }

        internal virtual Tween<T1, T2> SetSpace(Space relativeTo)
        {
            m_RelativeTo = relativeTo;
            return this;
        }

        internal virtual Tween<T1, T2> SetInterpolation(IInterpolator<T2> interpolator)
        {
            m_Interpolator = interpolator;
            return this;
        }

    }

    public class AnchoredPosition : Tween<RectTransform, Vector3>
    {
        internal AnchoredPosition(Func<RectTransform, Vector3> getter, Action<RectTransform, Vector3> setter) : base(getter, setter)
        {

        }

        public override void Initialise()
        {
            switch (m_RelativeTo)
            {
                case Space.Self:
                    m_EndValue += m_StartValue;
                    break;
                case Space.World:
                    break;
            }

            base.Reset();
        }
    }

    public class Animation : Tween<Image, Sprite>
    {
        private Sprite[] m_Sprites;

        private int m_NumberOfLoops;
        private int m_CurrentFrame;
        private int m_LoopDurationCounter;

        private float m_FrameRate;

        internal Animation(Func<Image, Sprite> getter, Action<Image, Sprite> setter) : base(getter, setter)
        {

        }

        public override void Initialise()
        {
            base.Initialise();

            m_CurrentFrame = 0;
            m_LoopDurationCounter = 0;

            m_Component.sprite = m_Sprites[m_CurrentFrame];
        }

        public override bool Update()
        {
            if (Time.time > m_TimeElapsed)
            {
                m_TimeElapsed = Time.time + (1f / m_FrameRate);

                m_CurrentFrame++;

                if (m_CurrentFrame >= m_Sprites.Length)
                {
                    m_LoopDurationCounter++;

                    if (m_NumberOfLoops > m_LoopDurationCounter)
                    {
                        m_CurrentFrame = 0;
                        m_Component.sprite = m_Sprites[m_CurrentFrame];

                        return true;
                    }

                    return false;
                }

                m_Component.sprite = m_Sprites[m_CurrentFrame];
            }

            return true;
        }

        public Animation SetSprites(params Sprite[] sprites)
        {
            m_Sprites = sprites;
            return this;
        }

        public Animation SetFrameRate(float frameRate)
        {
            m_FrameRate = frameRate;
            return this;
        }

        public Animation SetNumberOfLoops(int numberOfLoops)
        {
            m_NumberOfLoops = numberOfLoops;
            return this;
        }

    }
}

/*
             if ((1f / m_FrameRate) <= m_TimeElapsed)
            {
                m_FrameDurationCounter++;
                m_TimeElapsed -= 1f / m_FrameRate;

                m_Component.sprite = m_Sprites[m_CurrentFrame];

                if (m_FrameDurationCounter >= (1f / m_FrameRate))
                {
                    m_CurrentFrame++;

                    if (m_CurrentFrame >= m_Sprites.Length)
                    {
                        m_LoopDurationCounter++;

                        if (m_NumberOfLoops > m_LoopDurationCounter)
                        {
                            m_CurrentFrame = 0;
                            return true;
                        }

                        return false;
                    }

                    m_FrameDurationCounter = 0;
                }
            }


            if (Time.time > m_TimeElapsed)
            {
                m_TimeElapsed = Time.time + (1f / m_FrameRate);

                m_CurrentFrame++;

                if (m_CurrentFrame >= m_Sprites.Length)
                {
                    m_LoopDurationCounter++;

                    if (m_NumberOfLoops > m_LoopDurationCounter)
                    {
                        m_CurrentFrame = 0;
                        m_Component.sprite = m_Sprites[m_CurrentFrame];

                        return true;
                    }

                    return false;
                }

                m_Component.sprite = m_Sprites[m_CurrentFrame];
            }
 */ 