/*
 * Author:          Shaun Taylor
 * Description:     Main menu for navigating the application
 * Last Updated:    06/01/2013
 * Progress:        100% Complete
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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Reflection;
// Allows the application to use the second project within the solution.
using SharedContent;

namespace AWGP
{
    public class MainMenu : MenuScreen
    {
        Texture2D backgroundTexture;
        ScreensConfig scrConfig;
        ApplicationConfig appConfig;

        Vector2 v_currentuser;
        SpriteFont kootenay10Font;

        public MainMenu()
        {
            TransitionOffTime = TimeSpan.FromSeconds(0);

            Selected = Color.Black;
            UnSelected = Color.White;
        }

        public override void LoadContent()
        {
            // Reload the content manager and loads up the ScreensSettings.xml for reading
            ContentManager Content = ScreenManager.Game.Content;
            scrConfig = Content.Load<ScreensConfig>("ScreensSettings");
            appConfig = Content.Load<ApplicationConfig>("ApplicationSettings");

            // Loads the menu titles.
            // #### Should be loading the array of items, then passing them through the MenuEntriesText function.
            MenuEntriesText.Add(scrConfig.Menuoptions01);
            MenuEntriesText.Add(scrConfig.Menuoptions02);
            MenuEntriesText.Add(scrConfig.Menuoptions03);
            MenuEntriesText.Add(scrConfig.Menuoptions04);
            MenuEntriesText.Add(scrConfig.Menuoptions05);

            // Works out how many items are in the list and then distances itself from the bottom.
            int menudistancebottom = (35 * MenuEntriesText.Count);
            int actualmenudistance = 720 - menudistancebottom;
            StartPosition = new Vector2(50, actualmenudistance);

            backgroundTexture = Content.Load<Texture2D>(scrConfig.MainMenu_BGImage);
            MenuText = Content.Load<SpriteFont>(scrConfig.MenuFont);

            // Load the UI elements
            kootenay10Font = Content.Load<SpriteFont>("Fonts\\titlemenufont");
            base.LoadContent();
        }

        public override void Remove()
        {
            base.Remove();
            MenuEntriesText.Clear();
        }

        public override void MenuSelect(int menuselected)
        {
            ExitScreen();
            switch (menuselected)
            {
                case 0: ScreenManager.AddScreen(new JoshDemoStory()); break;
                case 1: ScreenManager.AddScreen(new PhysicsDemo()); break;
                case 2: ScreenManager.AddScreen(new LoadingScreen()); break;
                case 3: ScreenManager.AddScreen(new MainMenu()); showSettings(); break;
                case 4: ScreenManager.Game.Exit(); break;
            }
        }

        public void showSettings()
        {
            EditorForm editor = new EditorForm();
            editor.Show();
        }

        public override void MenuCancel()
        {
            ExitScreen();
        }

        public void DrawText()
        {
            // Loads the Application Settings XML file
            System.Xml.XmlDocument appConfigXML = new System.Xml.XmlDocument();
            System.Xml.XmlDocument serConfigXML = new System.Xml.XmlDocument();
            appConfigXML.Load(Game._path + "\\Content\\ApplicationSettings.xml");
            serConfigXML.Load(Game._path + "\\Content\\ServiceSettings.xml");

            // reloads the spritebatch
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.DrawString(kootenay10Font, "Welcome, " + appConfigXML.SelectSingleNode("//Username").InnerText, new Vector2(30, 30), Color.White);
        }

        public override void Draw(GameTime gameTime)
        {
            // Sets up the auto-scale resolution class
            Resolution.BeginDraw();
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            // SpriteBatch auto-scaling properties. Will scale based on current resolution on a matrix against the 1280x720 default
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());
            spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
            DrawText();
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
