/*Author - Josh
 * This is the base class for loading a sprite for use
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AWGP.Graphics.Sprites
{
    public class Sprite
    {
        protected Texture2D texture;
        protected Vector2 centre;
        public Vector2 screenPos;
        public Rectangle sourceRect;
        public Vector2 velocity;
        protected float rotation = 0.0f;
        protected float scale = 1.0f;

        public Sprite(Texture2D tex, Vector2 centre, Vector2 pos, Rectangle sourceRect, Vector2 vel)
        {
            texture = tex;
            this.centre = centre;
            this.screenPos = pos;
            this.sourceRect = sourceRect;
            this.velocity = vel;
        }

        public virtual void Update(GameTime gameTime, Rectangle viewportRect)
        {

        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Math.Round(screenPos.X) - sourceRect.Width / 2,
                    (int)Math.Round(screenPos.Y) - sourceRect.Height / 2,
                    sourceRect.Width, sourceRect.Height);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sb, Color col)
        {
            sb.Draw(texture, screenPos, sourceRect, col, rotation, centre, scale, SpriteEffects.None, 0);
        }

        public bool CollidesWith(Sprite sprite)
        {
            return this.CollidesWithCore(sprite);
        }

        protected virtual bool CollidesWithCore(Sprite sprite)
        {
            return this.BoundingBox.Intersects(sprite.BoundingBox);
        }
    }
}
