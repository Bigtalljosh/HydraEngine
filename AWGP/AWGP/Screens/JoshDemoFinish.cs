/*
 * Author:          Shaun Taylor
 * Description:     Displays the users final score for Joshs demo, then returns them back to the main
 *                  menu where they can try again.
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
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace AWGP
{
    public class JoshDemoFinish : GameScreen
    {
        SpriteFont currentscoreFont;
        Vector2 currentscorePosition;
        string currentscoreText;
        int currentscore = JoshDemo.currentscore;
        int newcurrentscore;
        Texture2D BackgroundTexture;


        public JoshDemoFinish()
        {
            TransitionOnTime = TimeSpan.FromSeconds(5); TransitionOffTime = TimeSpan.FromSeconds(4);
            //ScreenTime = TimeSpan.FromSeconds(0);
            //OpacityColor = Color.Black; Opacity = 0.0f;
        }

        public override void Initialize()
        {
            newcurrentscore = currentscore;
            currentscoreText = "" + newcurrentscore;
            currentscorePosition = new Vector2(775, 340);
            base.Initialize();
        }
        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            BackgroundTexture = content.Load<Texture2D>("Textures\\avoidance\\avoidancefinishscreen");
            currentscoreFont = content.Load<SpriteFont>("Fonts\\titlemenufont");
        }
        public override void Update(GameTime gameTime, bool covered)
        {
            InputManager input = ScreenManager.InputSystem;
            if (input.MenuSelect)
            {
                Remove();
            }
        }
        public override void Remove()
        {
            ScreenManager.AddScreen(new MainMenu());
            base.Remove();
        }
        public override void Draw(GameTime gameTime)
        {
            Resolution.BeginDraw();
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());
            spriteBatch.Draw(BackgroundTexture, Vector2.Zero, Color.White);
            spriteBatch.DrawString(currentscoreFont, "Final Score: " + currentscore, currentscorePosition, Color.White);
            spriteBatch.End();
        }
    }
}
