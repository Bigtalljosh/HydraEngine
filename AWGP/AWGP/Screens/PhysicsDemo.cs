//This is one of Bens classes. I'm not bothering to comment it for him as he has done no work except all this copy/paste code.
//He hasn't responded to any comunication from me and Shaun as recent, and in the past all we got was empty promises of 'work to be done soon'
//I added a line of code to the draw method stating myself and shaun take no responsibility for this

//This class I also believe Shaun fixed the code in order to give Ben a basis to start his work, Ben still did nothing.

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Resolvers;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Reflection;
// Allows the application to use the second project within the solution.
using SharedContent;
using AWGP.Input;

namespace AWGP
{
    public class PhysicsDemo : GameScreen
    {
        //-----------------------NEW STUFF-----------------------//

        //Set up the managers
        TextureManager textures = TextureManager.Instance;
        FontManager fonts = FontManager.Instance;
        SoundManager sounds = SoundManager.Instance;
        SpriteManager sprites = SpriteManager.Instance;

        //The font
        SpriteFont Kootenay10Font;

        //Textures used
        Texture2D carriageTexture;
        Texture2D cannonTexture;
        Texture2D rocketTexture;

        GameStates GameState;
        float KeyPressCheckDelay;
        float TotalElapsedTime = 0;

        enum GameStates
        { 
            Normal,
            Paused,
        }

        //-----------------------NEW STUFF-----------------------//


        //GraphicsDeviceManager graphics;
        //SpriteBatch spriteBatch;
        //GraphicsDevice device;

        //Texture2D backgroundTexture;
        //Texture2D foregroundTexture;
        //Player controlled images
        //Texture2D carriageTexture;
        //Texture2D cannonTexture;
        //Texture2D rocketTexture;
        //SpriteFont font;
        //int screenWidth;
        //int screenHeight;

        //Multiple players onscreen - Almost - Worms Esque
        PlayerData[] players;
        int numberOfPlayers = 3;
        float playerScaling = 1;
        int currentPlayer = 0;

        //Firing Projectiles
        bool rocketFlying = false;
        Vector2 rocketPosition;
        Vector2 rocketDirection;
        float rocketAngle;
        //float rocketScaling = 0.1f;

        public struct PlayerData
        {
            public Vector2 Position;
            public bool isAlive;
            public Color Color;
            //Important for the physics stuff to port over 
            public float Angle;
            public float Power;
        }

        public PhysicsDemo()
        {

        }

        public override void Initialize()
        {
            //Editing Window Size - Will be removed when port over to new engine framwork
            //graphics.PreferredBackBufferWidth = 500;
            //graphics.PreferredBackBufferHeight = 500;
            //graphics.IsFullScreen = false;
            //graphics.ApplyChanges();

            base.Initialize();
        }

        private void SetUpPlayers() //When setting up multiple players
        {
            Color[] playerColors = new Color[10];
            playerColors[0] = Color.Blue;


            players = new PlayerData[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players[i].isAlive = true;
                players[i].Color = playerColors[i];
                players[i].Angle = MathHelper.ToRadians(90);
                players[i].Power = 400;
            }

            players[0].Position = new Vector2(0, ScreenManager.GraphicsDevice.Viewport.Height);
        }

        public override void LoadContent()
        {
            //-----------------------NEW STUFF-----------------------//

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

            //-----------------------NEW STUFF-----------------------//


            // Create a new SpriteBatch, which can be used to draw textures.
            //spriteBatch = new SpriteBatch(GraphicsDevice);
            //device = graphics.GraphicsDevice;

            //ContentManager Content = ScreenManager.Game.Content;

            //backgroundTexture = Content.Load<Texture2D>("Textures\\background");
            //foregroundTexture = Content.Load<Texture2D>("Textures\\foreground");

            //carriageTexture = Content.Load<Texture2D>("Textures\\carriage");
            //cannonTexture = Content.Load<Texture2D>("Textures\\cannon");
            //rocketTexture = Content.Load<Texture2D>("Textures\\rocket");

            //font = Content.Load<SpriteFont>("Fonts\\titlemenufont");

            SetUpPlayers();

            playerScaling = 40.0f / (float)carriageTexture.Width;
        }

        public override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public override void Update(GameTime gameTime, bool covered)
        {
            //-----------------------NEW STUFF-----------------------//

            InputManager input = ScreenManager.InputSystem;

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            TotalElapsedTime += elapsed;

            //Josh originally put this quit function in. 
            KeyboardState keyboard = Keyboard.GetState();
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) || keyboard.IsKeyDown(Keys.F1))
            {
                Remove();
                ScreenManager.AddScreen(new MainMenu());
            }            

            if (GameState == GameStates.Normal)
            {
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

            //-----------------------NEW STUFF-----------------------//   

            ProcessKeyboard();

            UpdateRocket();

            base.Update(gameTime, covered);
        }

        private void UpdateRocket() //Physics effects on the rockets being fired.
        {
            if (rocketFlying)
            {
                //Gravity
                Vector2 gravity = new Vector2(0, 1);
                rocketDirection += gravity / 10.0f;
                rocketPosition += rocketDirection;

                //Wind Effect
                //Helps the rocket seem more believable in flight
                
                Vector2 wind = new Vector2(-0.8f, 0);
                rocketDirection += wind / 25.0f;
                rocketAngle = (float)Math.Atan2(rocketDirection.X, -rocketDirection.Y);
                
            }
        }

        private void ProcessKeyboard()
        {
            KeyboardState keybState = Keyboard.GetState();
            if (keybState.IsKeyDown(Keys.Left))
                players[currentPlayer].Angle -= 0.01f;
            if (keybState.IsKeyDown(Keys.Right))
                players[currentPlayer].Angle += 0.01f;

            if (players[currentPlayer].Angle > MathHelper.PiOver2)
                players[currentPlayer].Angle = -MathHelper.PiOver2;
            if (players[currentPlayer].Angle < -MathHelper.PiOver2)
                players[currentPlayer].Angle = MathHelper.PiOver2;

            if (keybState.IsKeyDown(Keys.Down))
                players[currentPlayer].Power -= 1;
            if (keybState.IsKeyDown(Keys.Up))
                players[currentPlayer].Power += 1;
            if (keybState.IsKeyDown(Keys.PageDown))
                players[currentPlayer].Power -= 20;
            if (keybState.IsKeyDown(Keys.PageUp))
                players[currentPlayer].Power += 20;

            if (players[currentPlayer].Power > 1000)
                players[currentPlayer].Power = 1000;
            if (players[currentPlayer].Power < 0)
                players[currentPlayer].Power = 0;

            //Controls for the rocket
            if (keybState.IsKeyDown(Keys.Enter) || keybState.IsKeyDown(Keys.Space))
            {
                rocketFlying = true;
                rocketPosition = players[currentPlayer].Position;
                rocketPosition.X += 20;
                rocketPosition.Y -= 10;
                rocketAngle = players[currentPlayer].Angle;
                Vector2 up = new Vector2(0, -1);
                Matrix rotMatrix = Matrix.CreateRotationZ(rocketAngle);
                rocketDirection = Vector2.Transform(up, rotMatrix);
                rocketDirection *= players[currentPlayer].Power / 50.0f;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Resolution.BeginDraw();
            // reloads the spritebatch
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());
            //DrawScenery();
            DrawPlayers();
            DrawText();
            DrawRocket();
            spriteBatch.DrawString(Kootenay10Font, "SHAUN AND MYSELF(JOSH) \nACCEPT NO RESPONSIBILTY FOR THIS PHYSICS DEMO \nPOTENTIALLY LOOKING VERY SIMILAR TO OTHERS \nBEN FAILED TO DO ANY WORK IN THIS MODULE \nAND THIS WAS HIS CONTRIBUTION OF CODE HE CLEARLY DID NOT DO \n PLEASE SEE COMMENTS IN THE CODE FOR ADDITIONAL INFO", new Vector2(100, 200), Color.Red);
            spriteBatch.DrawString(Kootenay10Font, "EDIT: Since posting the last message \nBen has done mimimal effort to hide his plagerism. \nThis consists of changing the background, \nmoving the player, \nchanging the player color.\n If anything I believe this shows he has the inititive and ability to learn \nthe code we produce so he has no excuse for doing nothing \nuntil 12 hours before hand in", new Vector2(100, 405), Color.Red);
            spriteBatch.End();
        }

        /*
        private void DrawScenery()
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
            spriteBatch.Draw(foregroundTexture, Vector2.Zero, Color.White);
        } 
         */

        private void DrawPlayers() //This basically loops through and adds a new carriage/cannon on screen for each of the players that are set up. 
        {
            foreach (PlayerData player in players)
            {
                if (player.isAlive)
                {
                    int xPos = (int)player.Position.X;
                    int yPos = (int)player.Position.Y;
                    Vector2 cannonOrigin = new Vector2(11,50); //11,50
                    // reloads the spritebatch
                    SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
                    spriteBatch.Draw(cannonTexture, new Vector2(xPos + 20, yPos - 10), null, player.Color, player.Angle, cannonOrigin, playerScaling, SpriteEffects.None, 1);
                    spriteBatch.Draw(carriageTexture, player.Position, null, player.Color, 0, new Vector2(0, carriageTexture.Height), playerScaling, SpriteEffects.None, 0);
                }

            }
        }

        private void DrawText()
        {
            PlayerData player = players[currentPlayer];
            int currentAngle = (int)MathHelper.ToDegrees(player.Angle);
            // reloads the spritebatch
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.DrawString(Kootenay10Font, "Cannon angle (LeftArrow/RightAarow): " + currentAngle.ToString(), new Vector2(20, 20), Color.Red);
            spriteBatch.DrawString(Kootenay10Font, "Cannon power (UpAarow/DownAarow): " + player.Power.ToString(), new Vector2(20, 45), Color.Red);            
            spriteBatch.DrawString(Kootenay10Font, "Fire with Enter Key", new Vector2(20, 70), Color.Red);
        }

        private void DrawRocket()
        {
            // reloads the spritebatch
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            if (rocketFlying)
                spriteBatch.Draw(rocketTexture, rocketPosition, null, players[currentPlayer].Color, rocketAngle, new Vector2(42, 240), 0.1f, SpriteEffects.None, 1);
        }
    }
}

