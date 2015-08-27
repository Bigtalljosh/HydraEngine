using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AWGP.Graphics.Sprites;

namespace AWGP.Graphics.Behaviours
{
    public class NormalBehaviour : Behaviour
    {

        protected override void BeginCore(AnimatedSprite subject)
        {
            // do nothing
        }

        protected override void UpdateCore(AnimatedSprite subject)
        {
            Rectangle viewportRect =
                SpriteManager.Instance.Game.GraphicsDevice.Viewport.Bounds;
            subject.screenPos += subject.velocity;

            // Check for collision with edges, if so, bounce 
            if (subject.screenPos.X + subject.sourceRect.Width / 2 > viewportRect.Right)
            {
                subject.velocity.X *= -1;
                subject.screenPos.X = viewportRect.Right - subject.sourceRect.Width / 2;
            }
            else if (subject.screenPos.X - subject.sourceRect.Width / 2 < viewportRect.Left)
            {
                subject.velocity.X *= -1;
                subject.screenPos.X = viewportRect.Left + subject.sourceRect.Width / 2;
            }
            else if (subject.screenPos.Y - subject.sourceRect.Height / 2 < viewportRect.Top)
            {
                subject.velocity.Y *= -1;
                subject.screenPos.Y = viewportRect.Top + subject.sourceRect.Height / 2;
            }
            else if (subject.screenPos.Y + subject.sourceRect.Height / 2 > viewportRect.Bottom)
            {
                subject.velocity.Y *= -1;
                subject.screenPos.Y = viewportRect.Bottom - subject.sourceRect.Height / 2;
            }
        }

        protected override void EndCore(AnimatedSprite subject)
        {
            // do nothing
        }
    }
}
