/*
 * Author:          Shaun Taylor
 * Description:     Behaviour main class, from Windows Game programming, allowing you to give
 *                  sprites different behaviours, such as a jitter effect or to have different
 *                  collision properties.
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
    public abstract class Behaviour:IBehaviour
    {
        protected Behaviour() { }

        public void Begin(AnimatedSprite subject) {  BeginCore(subject); }
        protected abstract void BeginCore(AnimatedSprite subject);

        public void Update(AnimatedSprite subject) { UpdateCore(subject); }
        protected abstract void UpdateCore(AnimatedSprite subject);

        public void End(AnimatedSprite subject) { EndCore(subject); }
        protected abstract void EndCore(AnimatedSprite subject);
    }
}
