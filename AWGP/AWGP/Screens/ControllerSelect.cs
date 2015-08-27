/*
 * Author:          Shaun Taylor
 * Description:     Controller select screen, should allow the user to select who to log in as.
 *                  Graphics depend on what input device is plugged in.
 * Last Updated:    06/01/2013
 * Progress:        75% Complete
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Resolvers;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml.Serialization;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Reflection;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;
// Allows the application to use the second project within the solution.
using SharedContent;

namespace AWGP
{
    public class ControllerSelectScreen : GameScreen
    {
        ScreensConfig scrConfig;
        Texture2D backgroundTexture, buttonTexture;
        string menuSelection = "";
        SpriteFont inputFont;
        GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

        public ControllerSelectScreen() { }

        public override void Initialize() { }

        public override void LoadContent()
        {
            ContentManager Content = ScreenManager.Game.Content;
            scrConfig = Content.Load<ScreensConfig>("ScreensSettings");
            inputFont = Content.Load<SpriteFont>("Fonts\\Koot14");

            TransitionOnTime = TimeSpan.FromSeconds(scrConfig.ControllerDetect_TranOn);
            TransitionOffTime = TimeSpan.FromSeconds(scrConfig.ControllerDetect_TranOff);

            if (gamePadState.IsConnected)
            {
                backgroundTexture = Content.Load<Texture2D>(scrConfig.ControllerDetect_BGImage);
                buttonTexture = Content.Load<Texture2D>(scrConfig.ControllerDetect_360Image);
            }
            else 
            {
                backgroundTexture = Content.Load<Texture2D>(scrConfig.ControllerDetect_BGImage);
                buttonTexture = Content.Load<Texture2D>(scrConfig.ControllerDetect_PCImage);
            }
        }


        public override void UnloadContent() { }


        public override void Update(GameTime gameTime, bool covered)
        {
            InputManager input = ScreenManager.InputSystem;                  // calls the menuinputsystem.c
            if (input.MoveMenuUp) { menuSelection = "Shaun"; }
            if (input.MenuCancel) { menuSelection = "Escape"; Remove(); }
            if (input.MenuSelect) { menuSelection = "Enter"; Remove(); }
            else { menuSelection = ""; Remove(); }
            base.Update(gameTime, covered);
        }


        public override void Remove()
        {
            if (menuSelection == "Escape") { base.Remove(); ScreenManager.Game.Exit(); }
            if (menuSelection == "Enter") { base.Remove(); ScreenManager.AddScreen(new MainMenu()); }
            else { base.Remove(); ScreenManager.AddScreen(new ControllerSelectScreen()); }
        }

        public override void Draw(GameTime gameTime)
        {
            Resolution.BeginDraw();
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Vector2 buttonTextureL = new Vector2(478, 386);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());

            spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
            spriteBatch.Draw(buttonTexture, buttonTextureL, Color.White);
            spriteBatch.DrawString(inputFont, "User Selected: " +  "\n [F1]Ben\n [F2]Josh\n [F3]Shaun", new Vector2(10, 10), Color.White);
            spriteBatch.End();
        }
        
    }
}