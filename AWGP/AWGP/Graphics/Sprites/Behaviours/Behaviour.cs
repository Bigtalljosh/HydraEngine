using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AWGP.Graphics.Sprites;

namespace AWGP.Graphics.Behaviours
{
    public abstract class Behaviour : IBehaviour
    {
        protected Behaviour()
        {

        }

        public void Begin(AnimatedSprite subject)
        {
            BeginCore(subject);
        }
        protected abstract void BeginCore(AnimatedSprite subject);

        public void Update(AnimatedSprite subject)
        {
            UpdateCore(subject);
        }
        protected abstract void UpdateCore(AnimatedSprite subject);

        public void End(AnimatedSprite subject)
        {
            EndCore(subject);
        }
        protected abstract void EndCore(AnimatedSprite subject);
    }
}
