/*
 * Author:          Shaun Taylor
 * Description:     Behaviour jitter behaviour, currently doesn't do anything, here as a placeholder
 *                  for any future ideas or expansion of rules for sprites.
 * Last Updated:    06/01/2013
 * Progress:        100% Complete
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AWGP;
using AWGP.Graphics.Sprites;

namespace AWGP
{
    public class JitterBehaviour:Behaviour
    {
        protected override void BeginCore(AnimatedSprite subject) { }

        protected override void UpdateCore(AnimatedSprite subject)
        {
            subject.screenPos += 5 * subject.velocity;
            subject.velocity.X *= -1;
            subject.velocity.Y *= -1;
        }

        protected override void EndCore(AnimatedSprite subject) { }
    }
}
