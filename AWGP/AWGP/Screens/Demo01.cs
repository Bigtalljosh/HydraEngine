/*
 * Author:          Shaun Taylor
 * Description:     Shauns main demo, was meant to use physics to be able to drag and drop
 *                  objects in to the scene and then have them interact with one another.
 * Last Updated:    06/01/2013
 * Progress:        100% Complete
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
    public class Demo01 : GameScreen
    {
        // Loads up all the necessary managers
        TextureManager textures = TextureManager.Instance;
        FontManager fonts = FontManager.Instance;
        SoundManager sounds = SoundManager.Instance;
        SpriteManager sprites = SpriteManager.Instance;
        
        // Movable UI elements, including the map, and action buttons
        Texture2D t_map, t_actionbuttons, t_ab_1, t_ab_2, t_ab_3, t_ab_4, t_ab_5;
        Vector2 v_map, v_actionbuttons, v_ab_1, v_ab_2, v_ab_3, v_ab_4, v_ab_5;

        Texture2D toolbox, tb_star, tb_pentagon, tb_circle, tb_square;
        Vector2 v_toolbox, v_star, v_pentagon, v_circle, v_square;
        
        // Fonts;
        SpriteFont koot10F, koot12F, koot14F, koot16F, koot18F;
        String currentInput;

        Vector2 v_mousepos, v_currentobj, v_paused;
        MouseState current_mouse;
        bool uiedit, squareSelect = false, starSelect = false, pentagonSelect = false, circleSelect = false;
        int mouse_x, mouse_y;

        GameStates GameState;
        float KeyPressCheckDelay = 0.2f;                                        // for escape last pressed (levelmenu)
        float TotalElapsedTime = 0;
        enum GameStates                                                         // allow the game to pause / unpause
        {
            Normal,                                                             // unpaused
            Paused,                                                             // paused
        }

        public Demo01()
        {
            // how many seconds do you want the transition to last for?
            TransitionOnTime = TimeSpan.FromSeconds(2);
            TransitionOffTime = TimeSpan.FromSeconds(1);
        }

        public override void Initialize()
        {
            // initiate the viewport
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            v_ab_1 = new Vector2(490, 650);
            v_mousepos = new Vector2(10, 690);
            v_currentobj = new Vector2(10, 30);
            v_paused = new Vector2(620, 350);
            v_toolbox = new Vector2(1090, 190);
            v_star = new Vector2(1110, 452);
            v_pentagon = new Vector2(1112, 375);
            v_square = new Vector2(1113, 217);
            v_circle = new Vector2(1115, 296);
        }

        public override void LoadContent()
        {
            // reload contentmanager
            ContentManager content = ScreenManager.Game.Content;
            textures.ContentManager = ScreenManager.Game.Content;
            sounds.ContentManager = ScreenManager.Game.Content;
            fonts.ContentManager = ScreenManager.Game.Content;
            // Load the UI elements
            t_ab_1 = content.Load<Texture2D>("Textures\\actionbutton_empty");

            textures.LoadTexture("toolbox", "Textures\\toolbox"); toolbox = textures.GetTextureByKey("toolbox");
            textures.LoadTexture("toolboxstar", "Textures\\tb_star"); tb_star = textures.GetTextureByKey("toolboxstar");
            textures.LoadTexture("toolboxpentagon", "Textures\\tb_pentagon"); tb_pentagon = textures.GetTextureByKey("toolboxpentagon");
            textures.LoadTexture("toolboxcircle", "Textures\\tb_circle"); tb_circle = textures.GetTextureByKey("toolboxcircle");
            textures.LoadTexture("toolboxsquare", "Textures\\tb_square"); tb_square = textures.GetTextureByKey("toolboxsquare");

            // Loads all the necessary fonts in to the font manager so they can be used when needed
            fonts.loadFont("koot10", "Fonts\\Koot10"); koot10F = fonts.GetFontByKey("koot10");
            fonts.loadFont("koot12", "Fonts\\Koot12"); koot12F = fonts.GetFontByKey("koot12");
            fonts.loadFont("koot14", "Fonts\\Koot14"); koot14F = fonts.GetFontByKey("koot14");
            fonts.loadFont("koot16", "Fonts\\Koot16"); koot16F = fonts.GetFontByKey("koot16");
            fonts.loadFont("koot18", "Fonts\\Koot18"); koot18F = fonts.GetFontByKey("koot18");
        }

        public override void UnloadContent()
        {

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

            //Quit key
            KeyboardState keyboard = Keyboard.GetState();
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) || keyboard.IsKeyDown(Keys.F1))
            {
                Remove();
                sprites.Sprites.Clear();
                cleanUp();
                ScreenManager.AddScreen(new MainMenu());
            }

            if (input.EnableEditUI == true)
            {
                currentInput = "Player Action 01";
            }

            if (GameState == GameStates.Normal)
            {
                // Enables and Disables the edit mode
                if (uiedit == false) { if (input.EnableEditUI) { uiedit = true; } }
                if (uiedit == true) { if (input.DisableEditUI) { uiedit = false; } }

                // Checks to see if there is mouse interaction above an object
                // If there is and edit mode == true, allow the object to be moved
                UpdateMouse();

                //
                //
                /*      Put all of your demostration stuff around here to allow it to run while the game 
                 *      is not paused. Let me know if you have issues working out how this works and I
                 *      will get back to you ASAP       -       Shaun
                 */
                //
                //


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

        protected void UpdateMouse()
        {
            current_mouse = Mouse.GetState();

            // The mouse x and y positions are returned relative to the
            // upper-left corner of the game window.

            mouse_x = current_mouse.X;
            mouse_y = current_mouse.Y;
        }

        private void DrawText()
        {
            //Reloads the spritebatch
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            System.Xml.XmlDocument keyConfigXML = new System.Xml.XmlDocument();
            keyConfigXML.Load(Game._path + "\\Content\\KeybindingsSettings.xml");

            if (uiedit == false)
            {
                spriteBatch.DrawString(koot14F, "Press '" + Convert.ToString(keyConfigXML.SelectSingleNode("//KBEnableEditUI").InnerText) + "' to enable edit UI mode", new Vector2(10, 10), Color.White);
            }
            if (uiedit == true)
            {
                spriteBatch.DrawString(koot14F, "Press '" + Convert.ToString(keyConfigXML.SelectSingleNode("//KBDisableEditUI").InnerText) + "' to disable edit UI mode", new Vector2(10, 10), Color.Red);
            }

            if (GameState == GameStates.Paused)
            {
                spriteBatch.DrawString(koot18F, "PAUSED", v_paused, Color.Red);
            }
            spriteBatch.DrawString(koot14F, "Mouse Position" + "   X: " + mouse_x.ToString() + "   Y: " + mouse_y.ToString(), v_mousepos, Color.White);
            spriteBatch.DrawString(koot14F, "Selected Object:", v_currentobj, Color.White);
            //spriteBatch.DrawString(koot14F, "Current key: " + currentInput, new Vector2(100, 100), Color.White);
        }

        private void MoveUI()
        {
            //Reloads the spritebatch
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            if (current_mouse.LeftButton == ButtonState.Pressed)
            {
                if (uiedit == true)
                {
                    if ((mouse_x > v_ab_1.X) && (mouse_x < (v_ab_1.X + t_ab_1.Width)) &&
                        (mouse_y > v_ab_1.Y) && (mouse_y < (v_ab_1.Y + t_ab_1.Height)))
                    {
                        v_ab_1 = new Vector2(mouse_x - (t_ab_1.Height / 2), mouse_y - (t_ab_1.Width / 2));
                        spriteBatch.Draw(t_ab_1, v_ab_1, Color.Green);
                    }
                    else
                    {
                        spriteBatch.Draw(t_ab_1, v_ab_1, Color.White);
                    }
                }
                if (uiedit == false)
                {
                    if ((mouse_x > v_ab_1.X) && (mouse_x < (v_ab_1.X + t_ab_1.Width)) &&
                                            (mouse_y > v_ab_1.Y) && (mouse_y < (v_ab_1.Y + t_ab_1.Height)))
                    {
                        spriteBatch.Draw(t_ab_1, v_ab_1, Color.Red);
                    }
                    else
                    {
                        spriteBatch.Draw(t_ab_1, v_ab_1, Color.White);
                    }
                }
            }
            else
            {
                spriteBatch.Draw(t_ab_1, v_ab_1, Color.White);
            }

        }

        public void Toolbox()
        {
            //Reloads the spritebatch
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Draw(toolbox, v_toolbox, Color.White);

            if (current_mouse.LeftButton == ButtonState.Pressed)
            {
                // SQUARE TOOLBOX
                if ((mouse_x > v_square.X) && (mouse_x < (v_square.X + tb_square.Width)) &&
                        (mouse_y > v_square.Y) && (mouse_y < (v_square.Y + tb_square.Height)))
                {
                    squareSelect = true;

                    if (pentagonSelect == false && starSelect == false && circleSelect == false)
                    {
                        v_square = new Vector2(mouse_x - (tb_square.Height / 2), mouse_y - (tb_square.Width / 2));
                        spriteBatch.Draw(tb_square, v_square, Color.Green);
                    }
                    else
                    {
                        spriteBatch.Draw(tb_square, v_square, Color.White);
                    }
                }
                else
                {
                    squareSelect = false;
                    spriteBatch.Draw(tb_square, v_square, Color.White);
                }

                // CIRCLE TOOLBOX
                if ((mouse_x > v_circle.X) && (mouse_x < (v_circle.X + tb_circle.Width)) &&
                        (mouse_y > v_circle.Y) && (mouse_y < (v_circle.Y + tb_circle.Height)))
                {
                    circleSelect = true;

                    if (squareSelect == false && starSelect == false && pentagonSelect == false)
                    {
                        v_circle = new Vector2(mouse_x - (tb_circle.Height / 2), mouse_y - (tb_circle.Width / 2));
                        spriteBatch.Draw(tb_circle, v_circle, Color.Green);
                    }
                    else
                    {
                        spriteBatch.Draw(tb_circle, v_circle, Color.White);
                    }
                }
                else
                {
                    circleSelect = false;
                    spriteBatch.Draw(tb_circle, v_circle, Color.White);
                }

                // PENTAGON TOOLBOX
                if ((mouse_x > v_pentagon.X) && (mouse_x < (v_pentagon.X + tb_pentagon.Width)) &&
                        (mouse_y > v_pentagon.Y) && (mouse_y < (v_pentagon.Y + tb_pentagon.Height)))
                {
                    pentagonSelect = true;
                    if (squareSelect == false && circleSelect == false && starSelect == false)
                    {
                        v_pentagon = new Vector2(mouse_x - (tb_pentagon.Height / 2), mouse_y - (tb_pentagon.Width / 2));
                        spriteBatch.Draw(tb_pentagon, v_pentagon, Color.Green);
                    }
                    else
                    {
                        spriteBatch.Draw(tb_pentagon, v_pentagon, Color.White);
                    }
                }
                else
                {
                    pentagonSelect = false;
                    spriteBatch.Draw(tb_pentagon, v_pentagon, Color.White);
                }

                // STAR TOOLBOX
                if ((mouse_x > v_star.X) && (mouse_x < (v_star.X + tb_star.Width)) &&
                        (mouse_y > v_star.Y) && (mouse_y < (v_star.Y + tb_star.Height)))
                {
                    starSelect = true;
                    if (squareSelect == false && circleSelect == false && pentagonSelect == false)
                    {
                        v_star = new Vector2(mouse_x - (tb_star.Height / 2), mouse_y - (tb_star.Width / 2));
                        spriteBatch.Draw(tb_star, v_star, Color.Green);
                    }
                    else
                    {
                        spriteBatch.Draw(tb_star, v_star, Color.White);
                    }
                }
                else
                {
                    starSelect = false;
                    spriteBatch.Draw(tb_star, v_star, Color.White);
                }
            }
            else
            {
                spriteBatch.Draw(tb_square, v_square, Color.White);
                spriteBatch.Draw(tb_circle, v_circle, Color.White);
                spriteBatch.Draw(tb_pentagon, v_pentagon, Color.White);
                spriteBatch.Draw(tb_star, v_star, Color.White);
            }
        }


        public override void Remove()
        {
            base.Remove();
        }

        public override void Draw(GameTime gameTime)
        {
            //Reloads the spritebatch
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Resolution.BeginDraw();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());
            Toolbox();
            DrawText();
            MoveUI();
            spriteBatch.End();
        }

        private void cleanUp()
        {
            fonts.clearManager();
            textures.clearManager();
            sounds.clearManager();
        }
    }
}

