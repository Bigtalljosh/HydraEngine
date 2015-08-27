/*
 * Author:          Shaun Taylor
 * Description:     Behaviour normal behaviour, sets up viewport collision which is used for Josh's Demo
 *                  no viewport collision enabled due to wanting the asteroids to go off the screen.
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
using Microsoft.Xna.Framework;

namespace AWGP
{
    public class NormalBehaviour:Behaviour
    {
        Rectangle viewportRect;

        protected override void BeginCore(AnimatedSprite subject) { }

        protected override void UpdateCore(AnimatedSprite subject)
        {
            //Rectangle viewportRect = SpriteManager.Instance.Game.GraphicsDevice.Viewport.Bounds;
            viewportRect = new Rectangle(0, 0, 1280, 720);
            subject.screenPos += subject.velocity;

            // Check for collision with right edge, if so, bounce 
            if (subject.screenPos.X + subject.sourceRect.Width / 2 > viewportRect.Right)
            {
                //subject.velocity.X *= +1;
                //subject.screenPos.X = (viewportRect.Left +40) - subject.sourceRect.Width / 2;
                
            }
            else if (subject.screenPos.X - subject.sourceRect.Width / 2 < viewportRect.Left)
            {
                //subject.velocity.X *= +1;
                //subject.screenPos.X = (viewportRect.Right -100) + subject.sourceRect.Width / 2;
            }
            else if (subject.screenPos.Y - subject.sourceRect.Height / 2 < (viewportRect.Top - 600))
            {
                //subject.velocity.Y *= -1;
                //subject.screenPos.Y = viewportRect.Top + subject.sourceRect.Height / 2;
            }
            else if (subject.screenPos.Y + subject.sourceRect.Height / 2 > viewportRect.Bottom)
            {
                //subject.screenPos.Y = viewportRect.Top - subject.sourceRect.Height / 2;
                //subject.velocity.Y *= -1;
                //subject.screenPos.Y = viewportRect.Bottom - subject.sourceRect.Height / 2;
            }
        }

        protected override void EndCore(AnimatedSprite subject) { }
    }
}
