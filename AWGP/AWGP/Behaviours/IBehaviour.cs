/*
 * Author:          Shaun Taylor
 * Description:     Behaviour interface class
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
    public interface IBehaviour
    {
        void Begin(AnimatedSprite subject);
        void Update(AnimatedSprite subject);
        void End(AnimatedSprite subject);
    }
}
