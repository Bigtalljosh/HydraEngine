/*
 * Author:          Shaun Taylor
 * Description:     Displays a menu in game when the corresponding key has been pressed.
 * Last Updated:    06/01/2013
 * Progress:        100% Complete
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace AWGP
{
    public class LevelMenu : MenuScreen
    {
        Texture2D backgroundTexture;
        Rectangle viewportRect;

        public LevelMenu()
        {
            TransitionOffTime = TimeSpan.FromSeconds(0);
            MenuEntriesText.Add("Return Game");
            //MenuEntriesText.Add("Back to Menu");  //<-- currently broke gives multiplemenus or only pauses game
            MenuEntriesText.Add("Exit Game");

            selected = Color.DarkOrange;
            UnSelected = Color.White;

            StartPosition = new Vector2(550, 330);
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            //backgroundTexture = content.Load<Texture2D>("Textures\\popup");
            MenuText = content.Load<SpriteFont>("Fonts\\titlemenufont");
            viewportRect = new Rectangle(0, 0, ScreenManager.Game.GraphicsDevice.Viewport.Width, ScreenManager.Game.GraphicsDevice.Viewport.Height);
            base.LoadContent();
        }

        public override void HandleInput()
        {
            InputManager input = ScreenManager.InputSystem;
            if (input.MenuCancel)
            {
                Remove();
            }
            base.HandleInput();
        }


        public override void Remove()
        {
            base.Remove();
            MenuEntriesText.Clear();
        }

        public override void MenuSelect(int menuselected)
        {
            Remove();
            switch (menuselected)
            {
                case 0: Remove(); break;
                //case 1: ScreenManager.AddScreen(new MainMenu()); Remove(); break;
                case 1: ScreenManager.Game.Exit(); break;
            }
        }

        public override void MenuCancel()
        {
            Remove();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Resolution.BeginDraw();
            spriteBatch.Begin();
            //spriteBatch.Draw(backgroundTexture, viewportRect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
