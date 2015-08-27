/*
 * Author:          Shaun Taylor
 * Description:     Moving load screen test, something different to loading up an image.
 * Last Updated:    06/01/2013
 * Progress:        75% Complete
 * 
 */
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

namespace AWGP
{
    public class LoadingScreen : GameScreen
    {
        // Loads up all the necessary managers
        TextureManager textures = TextureManager.Instance;
        FontManager fonts = FontManager.Instance;
        SoundManager sounds = SoundManager.Instance;
        SpriteManager sprites = SpriteManager.Instance;

        NormalBehaviour normalBehaviour;                                        // behaviour
        JitterBehaviour jitterBehaviour;

        // Textures
        Rectangle viewportRect;
        Texture2D loading;

        SpriteFont koot10Fload;

        float loadingScreenTime = 0;
        float loadingCountTime = 0;
        bool allownext = false;

        GameStates GameState;
        float KeyPressCheckDelay = 0.2f;                                        // for escape last pressed (levelmenu)
        float TotalElapsedTime = 0;
        enum GameStates                                                         // allow the game to pause / unpause
        {
            Normal,                                                             // unpaused
            Paused,                                                             // paused
        }

        public LoadingScreen()
        {
            // how many seconds do you want the transition to last for?
            TransitionOnTime = TimeSpan.FromSeconds(5);
            TransitionOffTime = TimeSpan.FromSeconds(5);

            normalBehaviour = new NormalBehaviour();                            // behaviours
            jitterBehaviour = new JitterBehaviour();
        }

        public override void Initialize()
        {
            // initiate the viewport
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
        }

        public override void LoadContent()
        {
            // reload contentmanager
            ContentManager content = ScreenManager.Game.Content;
            textures.ContentManager = ScreenManager.Game.Content;
            sounds.ContentManager = ScreenManager.Game.Content;
            fonts.ContentManager = ScreenManager.Game.Content;

            //####JOSH EDIT####
            //Fixed the Error with the FontManager here the "Fonts\\Koot10" was set to "Fonts\\Koot18". It now however throws an error with the screen manager o_o
            // Loads all the necessary fonts in to the font manager so they can be used when needed
            fonts.loadFont("koot10load", "Fonts\\Koot14"); koot10Fload = fonts.GetFontByKey("koot10load");

            // background image
            textures.LoadTexture("loading", "Textures\\loading_bg"); loading = textures.GetTextureByKey("loading");

            // Loading text
            sprites.Sprites.Add(createGem(new Vector2(RandomNumber(109, 221), RandomNumber(-100, -50)), new Vector2(RandomNumber(0, 0), RandomNumber(2, 3))));
            sprites.Sprites.Add(createGem(new Vector2(RandomNumber(110, 321), RandomNumber(-200, -50)), new Vector2(RandomNumber(0, 0), RandomNumber(2, 4))));
            sprites.Sprites.Add(createGem(new Vector2(RandomNumber(322, 535), RandomNumber(-100, -50)), new Vector2(RandomNumber(0, 0), RandomNumber(2, 5))));
            sprites.Sprites.Add(createGem(new Vector2(RandomNumber(534, 746), RandomNumber(-200, -150)), new Vector2(RandomNumber(0, 0), RandomNumber(3, 3))));
            sprites.Sprites.Add(createGem(new Vector2(RandomNumber(746, 957), RandomNumber(-250, -100)), new Vector2(RandomNumber(0, 0), RandomNumber(2, 4))));
            sprites.Sprites.Add(createGem(new Vector2(RandomNumber(958, 1170), RandomNumber(-150, -50)), new Vector2(RandomNumber(0, 0), RandomNumber(2, 3))));

            sprites.Sprites.Add(createGem(new Vector2(RandomNumber(210, 421), RandomNumber(-400, -250)), new Vector2(RandomNumber(0, 0), RandomNumber(2, 5))));
            sprites.Sprites.Add(createGem(new Vector2(RandomNumber(422, 635), RandomNumber(-300, -250)), new Vector2(RandomNumber(0, 0), RandomNumber(2, 4))));
            sprites.Sprites.Add(createGem(new Vector2(RandomNumber(634, 846), RandomNumber(-400, -350)), new Vector2(RandomNumber(0, 0), RandomNumber(2, 3))));
            sprites.Sprites.Add(createGem(new Vector2(RandomNumber(846, 1057), RandomNumber(-350, -300)), new Vector2(RandomNumber(0, 0), RandomNumber(2, 4))));
            sprites.Sprites.Add(createGem(new Vector2(RandomNumber(1058, 1170), RandomNumber(-350, -250)), new Vector2(RandomNumber(0, 0), RandomNumber(2, 3))));

            sprites.Sprites.Add(createGem(new Vector2(RandomNumber(160, 371), RandomNumber(-600, -450)), new Vector2(RandomNumber(0, 0), RandomNumber(2, 4))));
            sprites.Sprites.Add(createGem(new Vector2(RandomNumber(372, 585), RandomNumber(-500, -450)), new Vector2(RandomNumber(0, 0), RandomNumber(3, 5))));
            sprites.Sprites.Add(createGem(new Vector2(RandomNumber(584, 796), RandomNumber(-600, -450)), new Vector2(RandomNumber(0, 0), RandomNumber(2, 4))));
            sprites.Sprites.Add(createGem(new Vector2(RandomNumber(796, 1007), RandomNumber(-550, -500)), new Vector2(RandomNumber(0, 0), RandomNumber(3, 4))));
            sprites.Sprites.Add(createGem(new Vector2(RandomNumber(1008, 1170), RandomNumber(-550, -450)), new Vector2(RandomNumber(0, 0), RandomNumber(2, 4))));

            viewportRect = new Rectangle(0, 0, ScreenManager.Game.GraphicsDevice.Viewport.Width, ScreenManager.Game.GraphicsDevice.Viewport.Height);
        }

        private AnimatedSprite createGem(Vector2 position, Vector2 velocity)    // blue gem method
        {
            ContentManager content = ScreenManager.Game.Content;
            textures.ContentManager = ScreenManager.Game.Content;
            sounds.ContentManager = ScreenManager.Game.Content;
            fonts.ContentManager = ScreenManager.Game.Content;
            Texture2D enemygemTexture = content.Load<Texture2D>("Textures\\loading_w");
            AnimatedSprite d = new AnimatedSprite(
                enemygemTexture,
                new Vector2(105, 15),
                position,
                new Rectangle(0, 0, 210, 30),
                velocity,                                                       // allows random velocity
                1, 1, 1);                                                       // tilesheet animation, rows, columns, frames
            d.Behaviour = normalBehaviour;                                      // sets behaviour
            return d;
        }

        public override void UnloadContent()
        {
            fonts.RemoveFontByKey("koot10load");
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void Update(GameTime gameTime, bool covered)
        {
            InputManager input = ScreenManager.InputSystem;
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;       // game time
            TotalElapsedTime += elapsed;
            loadingScreenTime += elapsed;
            loadingCountTime = (loadingScreenTime * 25);


            if (GameState == GameStates.Normal)
            {

                //
                //
                /*      Put all of your demostration stuff around here to allow it to run while the game 
                 *      is not paused. Let me know if you have issues working out how this works and I
                 *      will get back to you ASAP       -       Shaun
                 */
                //
                //

                //Quit key
                KeyboardState keyboard = Keyboard.GetState();
                if (allownext == true)
                {
                    if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) || keyboard.IsKeyDown(Keys.Enter))
                    {
                        Remove();
                        ScreenManager.AddScreen(new Demo01());
                    }
                }

                foreach (AnimatedSprite d in sprites.Sprites)
                {
                    d.Update(gameTime, viewportRect);                           // updates all sprites in list
                }


                // Only Update the game when the game is UNPAUSED
                base.Update(gameTime, covered);

                if (TotalElapsedTime >= KeyPressCheckDelay)
                {
                    if (input.MenuCancel)                                       // if escape is pressed
                    {
                        GameState = GameStates.Paused;                          // pause game
                        TotalElapsedTime = 0.0f;
                        //ScreenManager.AddScreen(new DemoMenuOptions());       // display ingame menu
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


        static int RandomNumber(int min, int max)                               // method to let anything have a random int
        {
            Random random = new Random();
            return random.Next(min, max);                                       // sets min and max declarations
        }

        private void DrawLoading(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Draw(loading, Vector2.Zero, Color.White);
            foreach (AnimatedSprite d in sprites.Sprites)
            {
                d.Draw(gameTime, ScreenManager.SpriteBatch, Color.White);
            }
            if (loadingCountTime < 100)
            {
                ScreenManager.SpriteBatch.DrawString(koot10Fload, "Loading: " + loadingCountTime + "% complete!", new Vector2(490, 350), Color.White);
            }
            if (loadingCountTime > 100)
            {
                ScreenManager.SpriteBatch.DrawString(koot10Fload, "Loading: " + "100% complete!", new Vector2(490, 340), Color.White);
                ScreenManager.SpriteBatch.DrawString(koot10Fload, "Press ENTER to contine", new Vector2(495, 360), Color.White);
                allownext = true;
            }
        }

        public override void Remove()
        {
            base.Remove();
        }

        public override void Draw(GameTime gameTime)
        {
            // reloads the spritebatch
            Resolution.BeginDraw();
            ScreenManager.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());
            DrawLoading(gameTime);
            ScreenManager.SpriteBatch.End();
        }
    }
}

