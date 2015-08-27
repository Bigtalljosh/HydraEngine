/* Author - Josh
 * This class is used in order to create a sprite with an animation.
 * It extends the class 'Sprite'
 */ 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AWGP.Graphics.Sprites
{
    public class AnimatedSprite : Sprite
    {
        private int rows;
        private int columns;
        private int frames;
        private int currentFrame;

        private Behaviour behaviour;
        public Behaviour Behaviour
        {
            get
            {
                return behaviour;
            }
            set
            {
                if (behaviour != null)
                    behaviour.End(this);
                behaviour = value;
                behaviour.Begin(this);
            }
        }

        public AnimatedSprite(Texture2D tex, Vector2 centre, Vector2 pos, Rectangle sourceRect, Vector2 vel, int rows, int columns, int frames)
            : base(tex, centre, pos, sourceRect, vel)
        {
            this.rows = rows;
            this.columns = columns;
            this.frames = frames;
            currentFrame = -1;
        }

        public override void Update(GameTime gameTime, Rectangle viewportRect)
        {
            base.Update(gameTime, viewportRect);
            currentFrame++;
            currentFrame %= frames;
            sourceRect.X = (currentFrame % columns) * sourceRect.Width;
            sourceRect.Y = (currentFrame / columns) * sourceRect.Height;
            if (behaviour != null)
                behaviour.Update(this);
        }
    }
}