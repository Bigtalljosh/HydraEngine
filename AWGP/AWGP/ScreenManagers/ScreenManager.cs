﻿/*
 * Author:          Shaun Taylor
 * Description:     Sets up the main screen manager as well as the ability to add or remove
 *                  screens from the list.
 * Last Updated:    06/01/2013
 * Progress:        100% Complete
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SharedContent;

namespace AWGP
{
    public class ScreenManager : DrawableGameComponent
    {
        // Set up Managers
        TextureManager textures = TextureManager.Instance;
        FontManager fonts = FontManager.Instance;
        SoundManager sounds = SoundManager.Instance;
        SpriteManager sprites = SpriteManager.Instance;

        // Creates a new empty of GameScreens called screen
        public List<GameScreen> screens = new List<GameScreen>();
        List<GameScreen> screensToUpdate = new List<GameScreen>();

        // Allows the spritebatch to be used on all screens
        // Also sets up a inputsystem for navigating menus
        SpriteBatch spriteBatch;
        InputManager inputSystem;
        Rectangle viewportScreen;
        bool isInitialized;

        // Allows other classes to access the spritebatch and input system
        public SpriteBatch SpriteBatch { get { return spriteBatch; } }
        public InputManager InputSystem { get { return inputSystem; } }
        public static KeybindingsConfig KeyBindings { get; private set; }

        public ScreenManager(Game game) : base(game)
        {
            base.Initialize();
            isInitialized = true;

            // Sets up a input system
            inputSystem = new InputManager();
        }

        protected override void LoadContent()
        {
            // # # # # # # # # # # # # # # # # # # # # # # # # #
            // Loads the necessary resource managers
            // # # # # # # # # # # # # # # # # # # # # # # # # #
            textures.ContentManager = Game.Content;
            sounds.ContentManager = Game.Content;
            fonts.ContentManager = Game.Content;
            ContentManager content = Game.Content;
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load screen dedicated content
            foreach (GameScreen screen in screens) { screen.LoadContent(); }
        }

        protected override void UnloadContent()
        {
            // Unloads screen dedicated content
            foreach (GameScreen screen in screens) { screen.UnloadContent(); fonts.clearManager(); textures.clearManager(); sounds.clearManager(); sprites.Sprites.Clear(); }
        }

        public override void Update(GameTime gameTime)
        {
            inputSystem.Update(gameTime);

            // Clear out the screensToUpdate list to copy the screens list
            // This allows us to add or remove screens without complaining
            screensToUpdate.Clear();

            // If there are ever 0 screens on the stack, then close the game
            if (screens.Count == 0) { this.Game.Exit(); }
            foreach (GameScreen screen in screens) { screensToUpdate.Add(screen); }

            bool screenIsCovered = false;
            bool firstScreen = true;

            if (!Game.IsActive)
            {
                // If the game isn't the active window, then pause everything.
            }
            else
            {
                while (screensToUpdate.Count > 0)
                {
                    GameScreen screen = screensToUpdate[screensToUpdate.Count - 1];
                    screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

                    // Update the screen
                    screen.Update(gameTime, screenIsCovered);

                    if (screen.IsActive)
                    {
                        if (firstScreen) { screen.HandleInput(); firstScreen = false; }
                        if (!screen.IsPopup) { screenIsCovered = true; }
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (GameScreen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden) { continue; }
                screen.Draw(gameTime);
            }
        }

        public void AddScreen(GameScreen screen)
        {
            // Allows you to add a screen to the stack, and load any necessary content
            screen.ScreenManager = this;
            if (this.isInitialized) { screen.LoadContent(); screen.Initialize(); }
            screens.Add(screen);
        }

        public void RemoveScreen(GameScreen screen)
        {
            // Allows you to remove a screen from the stack, which also stops it from updating
            // Remember to add .dispose to the unloadcontent method on each screen so content can
            // actually be removed from memory.
            if (this.isInitialized) { screen.UnloadContent(); }
            screens.Remove(screen);
            screensToUpdate.Remove(screen);
        }
    }
}
