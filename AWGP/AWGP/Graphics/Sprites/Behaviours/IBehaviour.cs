using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AWGP.Graphics.Sprites;

namespace AWGP.Graphics.Behaviours
{
    public interface IBehaviour
    {
        void Begin(AnimatedSprite subject);
        void Update(AnimatedSprite subject);
        void End(AnimatedSprite subject);
    }
}
