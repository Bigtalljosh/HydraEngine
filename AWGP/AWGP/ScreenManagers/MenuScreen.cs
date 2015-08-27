/*
 * Author:          Shaun Taylor
 * Description:     Sets up the menu screen child of the screen manager, the sound to be played when
 *                  navigating any menu as well as setting up where and how the text for the menu
 *                  is to be displayed on the screen.
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
using SharedContent;

namespace AWGP
{
    public abstract class MenuScreen : GameScreen
    {
        public static KeybindingsConfig KeyBindings { get; private set; }
        SoundManager sounds = SoundManager.Instance;
        FontManager fonts = FontManager.Instance;

        // Sets up a list to store all the strings
        List<String> menuentriesText = new List<String>();
        public List<String> MenuEntriesText { get { return menuentriesText; } }

        SpriteFont menuText;
        public SpriteFont MenuText { get { return menuText; } set { menuText = value; } }

        // Starting position on the screen in X and Y
        Vector2 startPosition;
        public Vector2 StartPosition { get { return startPosition; } set { startPosition = value; } }

        Vector2 currentPosition;
        public Vector2 CurrentPosition { get { return currentPosition; } set { currentPosition = value; } }

        // Colour of the text when the cursor is over it
        public Color selected;
        public Color Selected { get { return selected; } set { selected = value; } }

        // Colour of the text when the cursor isn't on it
        Color unselected;
        public Color UnSelected { get { return unselected; } set { unselected = value; } }

        // Sets up instances to allow sounds to be played on the menu screens
        SoundEffect menumovesound, menuselectsound;
        SoundEffectInstance menumovesoundInstance, menuselectsoundInstance;
        float globalVolume = 0.70f;

        // Sets up the ability to select a menu item based on it's ID
        int selectedEntry = 0;
        public abstract void MenuSelect(int menuselected);
        public abstract void MenuCancel();

        public MenuScreen()
        {
            // Default time it takes for the screen to fade in and out
            TransitionOnTime = TransitionOffTime = TimeSpan.FromSeconds(2);
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            sounds.ContentManager = ScreenManager.Game.Content;
            fonts.ContentManager = ScreenManager.Game.Content;

            // Loads the sound for menu navigation
            sounds.LoadSoundEffect("menumove", "Audio\\menus\\menumovesound");
            menumovesound = sounds.GetSoundEffectByKey("menumove");
            menumovesoundInstance = menumovesound.CreateInstance();
            menumovesoundInstance.Volume = globalVolume;

            sounds.LoadSoundEffect("menuselect", "Audio\\menus\\menuselectsound");
            menuselectsound = sounds.GetSoundEffectByKey("menuselect");
            menuselectsoundInstance = menuselectsound.CreateInstance();
            menuselectsoundInstance.Volume = globalVolume;
        }

        public override void UnloadContent()
        {
            if (menuText != null) menuText = null;
        }

        public override void HandleInput()
        {
            // Loads up the input system that can be used to control the menu
            InputManager input = ScreenManager.InputSystem;
            if (input.MoveMenuUp)
            {
                selectedEntry--;
                menumovesoundInstance.Volume = globalVolume;
                menumovesoundInstance.Play();
                if (selectedEntry < 0)
                {
                    selectedEntry = menuentriesText.Count - 1;
                }
            }
            if (input.MoveMenuDown)
            {
                selectedEntry++;
                menumovesoundInstance.Volume = globalVolume;
                menumovesoundInstance.Play();
                if (selectedEntry >= menuentriesText.Count)
                {
                    selectedEntry = 0;
                }
            }
            if (input.MenuSelect)
            {
                menuselectsoundInstance.Volume = globalVolume;
                menuselectsoundInstance.Play();
                MenuSelect(selectedEntry);
            }
        }

        public override void Update(GameTime gameTime, bool covered)
        {
            base.Update(gameTime, covered);
            currentPosition = new Vector2(startPosition.X, startPosition.Y);
            if (ScreenState == ScreenState.TransitionOn || ScreenState == ScreenState.TransitionOff)
            {
                Vector2 acceleration = new Vector2((float)Math.Pow(TransitionPercent - 1, 2), 0);
                acceleration.X *= TransitionDirection * 150;
                currentPosition += acceleration;
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Vector2 menuPosition = new Vector2(currentPosition.X, currentPosition.Y);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());
            for (int i = 0; i < menuentriesText.Count; i++)
            {
                bool isSelected = (i == selectedEntry);
                DrawTextEntry(spriteBatch, gameTime, menuentriesText[i], menuPosition, isSelected);
                menuPosition.Y += menuText.LineSpacing;
            }
            spriteBatch.End();
        }

        private void DrawTextEntry(SpriteBatch spriteBatch, GameTime gameTime, string textEntry, Vector2 position, bool isSelected)
        {
            Vector2 origin = new Vector2(0, menuText.LineSpacing / 2);
            Color color = isSelected ? selected : unselected;
            color = (color * ScreenAlpha);

            float pulse = (float)(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 3));
            float scale = isSelected ? (1 + pulse * 0.05f) : 0.8f;

            spriteBatch.DrawString(menuText, textEntry, position, color, 0, origin, scale, SpriteEffects.None, 0);
        }
    }
}
