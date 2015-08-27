using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using SharedContent;
using AWGP.Graphics.Sprites;

/* Ben - So, I've basically rewritten this entire demo. I've taken some stuff from the tutorials I found at WEBSITE and
 * i've followed the structure of Shauns/Josh's demos for getting stuff working.
 * Basically, it has acceleration, gravity, wind in a similar vein to the previous demo.
 */

namespace AWGP
{
    public class NewPhysicsDemo : GameScreen
    {
        #region Fields   
        //SpriteBatch spriteBatch;

        public struct cannonData
        {
            public Vector2 Position;
            public float Angle;
            public float Power;
        }

        cannonData[] Cannon;
        int numberOfCannons = 1;
        int currentPlayer = 0;

        //Textures used
        Texture2D carriageTexture;
        Texture2D cannonTexture;
        Texture2D rocketTexture;

        //Projectile info
        bool rocketFlying = false;
        Vector2 rocketPosition;
        Vector2 rocketDirection;
        float rocketAngle;
        float rocketScaling = 0.1f;        

        //Set up the managers
        TextureManager textures = TextureManager.Instance;
        FontManager fonts = FontManager.Instance;
        SoundManager sounds = SoundManager.Instance;
        SpriteManager sprites = SpriteManager.Instance;        

        SpriteFont Kootenay10Font;

        GameStates GameState;
        float KeyPressCheckDelay = 0.2f;                                        // for escape last pressed (levelmenu)
        float TotalElapsedTime = 0;
        enum GameStates
        {
            Normal,
            Paused,
        }

        #endregion  

        public NewPhysicsDemo()
        {
            TransitionOnTime = TimeSpan.FromSeconds(2);
            TransitionOffTime = TimeSpan.FromSeconds(1);
        }

        public override void Initialize()
        {
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            textures.ContentManager = ScreenManager.Game.Content;
            fonts.ContentManager = ScreenManager.Game.Content;

            //Text
            Kootenay10Font = content.Load<SpriteFont>("Fonts\\titlemenufont");

            //Textures
            textures.LoadTexture("cannon", "Textures\\cannon");
            textures.LoadTexture("carriage", "Textures\\carriage");
            textures.LoadTexture("rocket", "Textures\\rocket");
            cannonTexture = textures.GetTextureByKey("cannon");
            carriageTexture = textures.GetTextureByKey("carriage");
            rocketTexture = textures.GetTextureByKey("rocket");

           //setUpCannon();
        }

        public override void UnloadContent()
        { }

        public override void Update(GameTime gameTime, bool covered)
        {
            InputManager input = ScreenManager.InputSystem;            

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            TotalElapsedTime += elapsed;

            if(GameState == GameStates.Normal)
            {
                //Rest of update logic needs to go here for the physics shit

                base.Update(gameTime, covered);

                if (TotalElapsedTime >= KeyPressCheckDelay)
                {
                    if (input.MenuCancel)
                    {
                        GameState = GameStates.Paused;
                        TotalElapsedTime = 0.0f;
                    }
                }
            }
            else
            {
                if (TotalElapsedTime >= KeyPressCheckDelay)
                {
                    // UnPause the current game
                    if (input.MenuCancel || input.MenuSelect)                   // if escape or enter is pressed
                    {
                        GameState = GameStates.Normal;                          // resume game
                        TotalElapsedTime = 0.0f;
                    }
                }
            }
        }

        public override void Remove()
        {
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            if (GameState == GameStates.Normal)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());
                drawCannon();
                spriteBatch.DrawString(Kootenay10Font, "Fire The Cannon", new Vector2(0, 10), Color.Blue);
                drawRocket();
                spriteBatch.End();
            }
            if (GameState == GameStates.Paused)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());
                spriteBatch.DrawString(Kootenay10Font, "Paused", new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2, ScreenManager.GraphicsDevice.Viewport.Height / 2), Color.Red);
                spriteBatch.End();
            }
        }

        public void drawCannon()
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Draw(carriageTexture, new Vector2(0, ScreenManager.GraphicsDevice.Viewport.Height - carriageTexture.Height), Color.White);
            spriteBatch.Draw(cannonTexture, new Vector2(40, ScreenManager.GraphicsDevice.Viewport.Height - cannonTexture.Height), Color.White);
        }

        public void updateRocket()
        {
            if (rocketFlying)
            {
                Vector2 gravity = new Vector2(0, 1);
                rocketDirection += gravity / 10.0f;
                rocketPosition += rocketDirection;
            }
        }

        public void controls()
        {
            KeyboardState kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Enter))
            {
                rocketFlying = true;
                rocketPosition = Cannon[currentPlayer].Position;
                rocketPosition.X += 20;
                rocketPosition.Y -= 20;
                rocketAngle = Cannon[currentPlayer].Angle;
                Vector2 up = new Vector2(0, -1);
                Matrix rotationMatrix = Matrix.CreateRotationZ(rocketAngle);
                rocketDirection = Vector2.Transform(up, rotationMatrix);
                rocketDirection *= Cannon[currentPlayer].Power / 50.0f;

            }
 
        }

        public void drawRocket()
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            if (rocketFlying)
                spriteBatch.Draw(rocketTexture, rocketPosition, null, Color.White, rocketAngle, new Vector2(50, 50), 0.1f, SpriteEffects.None, 1);
        }
        
        /* SET UP CANNON
        public void setUpCannon()
        {
            Cannon = new cannonData[numberOfCannons];
            for (int i = 0; i < numberOfCannons; i++)
            {
                Cannon[i].Angle = MathHelper.ToRadians(90);
                Cannon[i].Power = 100;
                Cannon[i].Position = new Vector2(0, ScreenManager.GraphicsDevice.Viewport.Height - carriageTexture.Height);

                foreach (cannonData cannon in Cannon)
                {
                    int xPos = (int)cannon.Position.X;
                    int yPos = (int)cannon.Position.Y;
                    /*
                    SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
                    spriteBatch.Draw(carriageTexture, new Vector2(0, ScreenManager.GraphicsDevice.Viewport.Height - carriageTexture.Height), Color.White);
                    spriteBatch.Draw(cannonTexture, new Vector2(40, ScreenManager.GraphicsDevice.Viewport.Height - cannonTexture.Height), Color.White);
                    
                }
        

            }
        }*/
    }
}
