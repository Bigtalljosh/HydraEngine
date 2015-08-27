/*Author - Josh
 * This class is used to create a sprite that a human player can control.
 * It extends the base class 'Sprite'
 */ 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AWGP.Graphics.Sprites
{
    public class UserControlledSprite : Sprite
    {
        private KeyboardState lastKeyboardState;

        bool screencrashbool = false;

        public static int screencrashcheck;
        public static int screenCrashCheck
        {
            get { return screencrashcheck; }
            set { screencrashcheck = value; }
        }

        public UserControlledSprite(Texture2D tex, Vector2 centre, Vector2 pos, Rectangle sourceRect, Vector2 vel) :
            base(tex, centre, pos, sourceRect, vel) { }

        public override void Update(GameTime gameTime, Rectangle viewportRect)
        {
            ProcessInput();
            ProcessColission(viewportRect);
            base.Update(gameTime, viewportRect);

            screenPos += velocity;
            velocity *= 0.95f;

            // Check for collision with right edge, if so, bounce 
            if (screenPos.X + sourceRect.Width / 2 > viewportRect.Right)
            {
                screencrashcheck = 1;
                screencrashbool = true;
                velocity.X *= -1;
                screenPos.X = viewportRect.Right - sourceRect.Width / 2;
            }
            else if (screenPos.X - sourceRect.Width / 2 < viewportRect.Left)
            {
                screencrashcheck = 1;
                screencrashbool = true;
                velocity.X *= -1;
                screenPos.X = viewportRect.Left + sourceRect.Width / 2;
            }
            else if (screenPos.Y - sourceRect.Height / 2 < viewportRect.Top)
            {
                screencrashcheck = 1;
                screencrashbool = true;
                velocity.Y *= -1;
                screenPos.Y = viewportRect.Top + sourceRect.Height / 2;
            }
            else if (screenPos.Y + sourceRect.Height / 2 > viewportRect.Bottom)
            {
                screencrashcheck = 1;
                screencrashbool = true;
                velocity.Y *= -1;
                screenPos.Y = viewportRect.Bottom - sourceRect.Height / 2;
            }
            else { screencrashcheck = 0; screencrashbool = false; }
        }

        protected void ProcessColission(Rectangle viewportRect)
        {
            if (screenPos.X + sourceRect.Width / 2 > viewportRect.Right)
            {
                velocity.X *= -1;
                screenPos.X = viewportRect.Right - sourceRect.Width / 2;
            }
            else if (screenPos.X - sourceRect.Width / 2 < viewportRect.Left)
            {
                velocity.X *= -1;
                screenPos.X = viewportRect.Left + sourceRect.Width / 2;
            }
            else if (screenPos.Y - sourceRect.Height / 2 < viewportRect.Top)
            {
                velocity.Y *= -1;
                screenPos.Y = viewportRect.Top + sourceRect.Height / 2;
            }
            else if (screenPos.Y + sourceRect.Height / 2 > viewportRect.Bottom)
            {
                velocity.Y *= -1;
                screenPos.Y = viewportRect.Bottom - sourceRect.Height / 2;
            }
        }

        protected void ProcessInput()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            velocity.X += gamePadState.ThumbSticks.Right.X;
            velocity.Y -= gamePadState.ThumbSticks.Right.Y;
            rotation += gamePadState.ThumbSticks.Left.X * 0.1f;
            if (gamePadState.IsButtonDown(Buttons.LeftShoulder))
            {
                scale *= 1.1f;
            }
            // control vibration using triggers
            GamePad.SetVibration(PlayerIndex.One, gamePadState.Triggers.Left, gamePadState.Triggers.Right);

#if !XBOX
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.A))
            {
                velocity.X -= 0.5f;
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                velocity.X += 0.5f;
            }
            else if (keyboardState.IsKeyDown(Keys.W))
            {
                velocity.Y -= 0.5f;
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                velocity.Y += 0.5f;
            }

            lastKeyboardState = keyboardState;

            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                screenPos.X = mouseState.X;
                screenPos.Y = mouseState.Y;
            }
#endif
        }
    }
}