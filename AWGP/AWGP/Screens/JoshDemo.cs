/* Author - Josh
 * This is my demo class to show of the use of the various managers I've created for this project
 * Audio, Texture, Font are all managers that handle there respective resources
 * This allows for the mapping of said resource to a 'key'
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
using Microsoft.Xna.Framework.Media;
using SharedContent;
using AWGP.Graphics.Sprites;

namespace AWGP
{
    public class JoshDemo : GameScreen
    {
        #region Fields
        SpriteBatch spriteBatch;
        Rectangle viewportRect;
        private Camera _camera;
        private List<Layer> _layers;
        private List<Layer> _layersinfront;
        Vector2 startPos;
        Vector2 startPos2;
        float zoomtimer = 0f;
        float zoominterval = 20f;                                                // miliseconds
        List<Sprite> Asteroid01 = new List<Sprite>(150);                     // list for blue gems
        int spriteelapsed = 0;

        // Set up Managers
        TextureManager textures = TextureManager.Instance;
        FontManager fonts = FontManager.Instance;
        SoundManager sounds = SoundManager.Instance;
        SpriteManager sprites = SpriteManager.Instance;

        // Sprites Behaviour
        NormalBehaviour normalBehaviour; JitterBehaviour jitterBehaviour;

        // Fixes the problem of buttons being "pressed" numerous times by adding a delay
        float KeyPressCheckDelay = 0.2f; float TotalElapsedTime = 0;

        // Allows you to pause and unpause the active screen
        GameStates GameState;
        enum GameStates { Normal, Paused, }

        //Bools
        bool playscreensound = false;
        bool drawGameOver = false;
        bool isColliding = false;

        // Scores
        public static int currentlives;             // allows other screens to access lives
        public static int CurrentLives { get { return currentlives; } set { currentlives = value; } }
        public static int currentscore;             // allows other screens to access score
        public static int CurrentScore { get { return currentscore; } set { currentscore = value; } }

        //Textures
        Texture2D backgroundTex, player, asteroid01;

        //Set up Fonts
        SpriteFont font1, font2;
        SpriteFont koot10F, koot12F, koot14F, koot16F, koot18F;

        //Set up Sounds
        SoundEffect background, shoot1, shoot2;
        SoundEffectInstance backgroundI, shoot1I, shoot2I;
        Song background2;
        float globalVolume = 0.70f;

        //Random Number Generator
        public Random randNum;

        //Misc
        float playerposX;
        float playerposY;

        #endregion    

        public JoshDemo()
        {
            // How many seconds do you want the transition to last for?
            TransitionOnTime = TimeSpan.FromSeconds(2); TransitionOffTime = TimeSpan.FromSeconds(1);
            // Sets up the behaviours
            normalBehaviour = new NormalBehaviour(); jitterBehaviour = new JitterBehaviour();
        }

        public override void Initialize()
        {
            // Initiate the viewport
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            drawGameOver = false;
            currentlives = 5; currentscore = 0;
        }

        public override void LoadContent()
        {
            // # # # # # # # # # # # # # # # # # # # # # # # # #
            // Loads the necessary resource managers
            // # # # # # # # # # # # # # # # # # # # # # # # # #
            ContentManager content = ScreenManager.Game.Content;
            textures.ContentManager = ScreenManager.Game.Content;
            sounds.ContentManager = ScreenManager.Game.Content;
            fonts.ContentManager = ScreenManager.Game.Content;

            // # # # # # # # # # # # # # # # # # # # # # # # # #
            // Sets up the camera layers and their velocity
            // # # # # # # # # # # # # # # # # # # # # # # # # #
            _camera = new Camera(ScreenManager.Game.GraphicsDevice.Viewport) { Limits = new Rectangle(0, 0, 173000, 900) };
            _layers = new List<Layer> {
                new Layer(_camera) { Parallax = new Vector2(0.03f, 0.5f) },
                new Layer(_camera) { Parallax = new Vector2(0.1f, 2.0f) },
                new Layer(_camera) { Parallax = new Vector2(0.1f, 2.0f) },
                new Layer(_camera) { Parallax = new Vector2(0.1f, 2.0f) }, };
            _layersinfront = new List<Layer> {
                new Layer(_camera) { Parallax = new Vector2(0.6f, 0.2f) },
                new Layer(_camera) { Parallax = new Vector2(0.6f, 0.2f) },
                new Layer(_camera) { Parallax = new Vector2(0.6f, 0.2f) },
                new Layer(_camera) { Parallax = new Vector2(0.6f, 0.2f) },
                new Layer(_camera) { Parallax = new Vector2(0.6f, 0.2f) }, };

            // # # # # # # # # # # # # # # # # # # # # # # # # #
            // Loads the necessary textures for the level
            // # # # # # # # # # # # # # # # # # # # # # # # # #
            _layers[0].Sprites.Add(new Sprite2 { Texture = content.Load<Texture2D>("Textures\\avoidance\\av_background") });
            textures.LoadTexture("player", "Textures\\avoidance\\av_player"); player = textures.GetTextureByKey("player");
            textures.LoadTexture("asteroid01", "Textures\\avoidance\\av_asteroid01"); asteroid01 = textures.GetTextureByKey("asteroid01");

            // # # # # # # # # # # # # # # # # # # # # # # # # #
            // Loads the necessary fonts for the level
            // # # # # # # # # # # # # # # # # # # # # # # # # #
            fonts.loadFont("bigfont", "Fonts\\josh\\big"); font1 = fonts.GetFontByKey("bigfont");
            fonts.loadFont("smallfont", "Fonts\\josh\\small"); font2 = fonts.GetFontByKey("smallfont");
            fonts.loadFont("koot10", "Fonts\\Koot10"); koot10F = fonts.GetFontByKey("koot10");
            fonts.loadFont("koot12", "Fonts\\Koot12"); koot12F = fonts.GetFontByKey("koot12");
            fonts.loadFont("koot14", "Fonts\\Koot14"); koot14F = fonts.GetFontByKey("koot14");
            fonts.loadFont("koot16", "Fonts\\Koot16"); koot16F = fonts.GetFontByKey("koot16");
            fonts.loadFont("koot18", "Fonts\\Koot18"); koot18F = fonts.GetFontByKey("koot18");

            // # # # # # # # # # # # # # # # # # # # # # # # # #
            // Loads the player sprites
            // # # # # # # # # # # # # # # # # # # # # # # # # #
            sprites.Player = new UserControlledSprite(player, new Vector2(player.Width / 2, player.Height / 2),
                new Vector2(200, 130), new Rectangle(0, 0, 200,130), new Vector2(1, 1) );

            // # # # # # # # # # # # # # # # # # # # # # # # # #
            // Loads the enemy sprites
            // # # # # # # # # # # # # # # # # # # # # # # # # #
            sprites.Sprites.Add(createAsteroid01(new Vector2(RandomNumber(1300, 1500), RandomNumber(100, 320)), new Vector2(RandomNumber(-1, -1), RandomNumber(0, 0))));
            sprites.Sprites.Add(createAsteroid01(new Vector2(RandomNumber(1301, 1400), RandomNumber(321, 620)), new Vector2(RandomNumber(-2, -1), RandomNumber(0, 0))));

            // # # # # # # # # # # # # # # # # # # # # # # # # #
            // Sets up the viewport and camera starting places
            // # # # # # # # # # # # # # # # # # # # # # # # # #
            viewportRect = new Rectangle(0, 0, ScreenManager.Game.GraphicsDevice.Viewport.Width, ScreenManager.Game.GraphicsDevice.Viewport.Height);
            startPos = new Vector2(0, 100);
            startPos2 = new Vector2(100, 100);
            _camera.Position = startPos;
            _camera.Zoom = 1.0f;

            //Josh Note: Had to move sound below player creation in order to set listener to player
            // # # # # # # # # # # # # # # # # # # # # # # # # #
            // Loads the necessary sounds for the level
            // # # # # # # # # # # # # # # # # # # # # # # # # #
            sounds.CreateListener("playerOne"); sounds.SetListenerPosition("playerOne", sprites.Player.screenPos);
            sounds.CreateEmitter("shoot"); sounds.SetEmitterPosition("shoot", new Vector2(100, 100));
            //Ideally the instance sound sent into the 3D is a repeating noise, such as crackling fire. The .isLooped() value should ideally be set to true also.
            //Set the emitter position to equal whatever is emitting the sound, so a fire sprite would emit the crackling noise for instance. 
            //Also need to set the sound to .Play();


            sounds.LoadSoundEffect("background", "Audio\\josh\\bg\\jdbackground"); background = sounds.GetSoundEffectByKey("background");
            backgroundI = background.CreateInstance(); backgroundI.Volume = globalVolume; backgroundI.IsLooped = true; backgroundI.Play();
            sounds.LoadSoundEffect("shoot1", "Audio\\josh\\sfx\\jdlaserblast1-1"); shoot1 = sounds.GetSoundEffectByKey("shoot1");
            shoot1I = shoot1.CreateInstance(); shoot1I.IsLooped = false; shoot1I.Volume = globalVolume;
            sounds.LoadSoundEffect("shoot2", "Audio\\josh\\sfx\\jdlaserblast2-1"); shoot2 = sounds.GetSoundEffectByKey("shoot2");
            shoot2I = shoot2.CreateInstance(); shoot2I.IsLooped = false; shoot2I.Volume = globalVolume;
            shoot2I.Apply3D(sounds.GetListenerByKey("playerOne"), sounds.GetEmitterByKey("shoot"));
        }

        private AnimatedSprite createAsteroid01(Vector2 position, Vector2 velocity)
        {
            AnimatedSprite d = new AnimatedSprite( asteroid01, new Vector2(75, 56), 
                position, new Rectangle(0, 0, 150, 113), velocity, 6, 6, 36);       // tilesheet animation, rows, columns, frames
            d.Behaviour = normalBehaviour;                                          // sets behaviour
            return d;
        }

        static int RandomNumber(int min, int max) { Random random = new Random(); return random.Next(min, max); }

        public override void UnloadContent() { }

        public override void Update(GameTime gameTime, bool covered)
        {
            // # # # # # # # # # # # # # # # # # # # # # # # # #
            // Assigns up all game time variables
            // # # # # # # # # # # # # # # # # # # # # # # # # #
            InputManager input = ScreenManager.InputSystem;
            zoomtimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            TotalElapsedTime += elapsed;
            spriteelapsed += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            // # # # # # # # # # # # # # # # # # # # # # # # # #
            // Allows you to force quit to the main menu
            // # # # # # # # # # # # # # # # # # # # # # # # # #
            KeyboardState keyboard = Keyboard.GetState();
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) || keyboard.IsKeyDown(Keys.F1))
            {
                backgroundI.Stop(); shoot1I.Stop();
                ScreenManager.AddScreen(new MainMenu());
                Remove(); cleanUp();
            }

            if (GameState == GameStates.Normal)
            {
                // # # # # # # # # # # # # # # # # # # # # # # # # #
                // Every 20ms, move the camera (set in fields)
                // # # # # # # # # # # # # # # # # # # # # # # # # #
                if (zoomtimer > zoominterval)
                {
                    _camera.Move(new Vector2(400.0f * elapsed, 0.0f), true);
                    zoomtimer = 0;
                }

                //Increase score as time progresses
                currentscore++;

                // # # # # # # # # # # # # # # # # # # # # # # # # #
                // Audio Controls: (Keyboard + Gamepad)
                // # # # # # # # # # # # # # # # # # # # # # # # # #
                if (input.AudioToggle) { audioToggle(); }       
                if (input.AudioVolumeUp) { audioVolumeIncrease(); }
                if (input.AudioVolumeDown) { audioVolumeDecrease(); }

                // # # # # # # # # # # # # # # # # # # # # # # # # #
                // Camera Controls: (Parallax Scrolling Effect)
                // # # # # # # # # # # # # # # # # # # # # # # # # #
                if (input.PlayerMoveRight) { _camera.Move(new Vector2(75.0f * elapsed, 0.0f), true); }
                if (input.PlayerMoveLeft) { _camera.Move(new Vector2(-50.0f * elapsed, 0.0f), true); }
                if (input.PlayerMoveDown) { _camera.Move(new Vector2(0.0f, 30.0f * elapsed), true); }
                if (input.PlayerMoveUp) { _camera.Move(new Vector2(0.0f, -30.0f * elapsed), true); }
                if (input.CameraZoomIn) { _camera.Zoom += 0.5f * elapsed; }
                if (input.CameraZoomOut) { _camera.Zoom -= 0.5f * elapsed; }
                if (input.CameraRotateRight) { _camera.Rotation += 1.5f * elapsed; }
                if (input.CameraRotateLeft) { _camera.Rotation -= 1.5f * elapsed; }
                if (input.CameraReset) { _camera.Position = startPos; _camera.Zoom = 1.0f; _camera.Rotation = 0.0f; }

                // # # # # # # # # # # # # # # # # # # # # # # # # #
                // Add new asteroids based on death/score
                // # # # # # # # # # # # # # # # # # # # # # # # # #
                if (sprites.Sprites.Count <= 1000)
                {
                    if (currentscore < 1999)                          // Slow speed
                    {
                        if (spriteelapsed % 1500 == 0)                // Add every 1.5 seconds
                        {
                            sprites.Sprites.Add(createAsteroid01(new Vector2(RandomNumber(1300, 1420), RandomNumber(100, 420)), new Vector2(RandomNumber(-2, -1), RandomNumber(0, 0))));
                            sprites.Sprites.Add(createAsteroid01(new Vector2(RandomNumber(1800, 2020), RandomNumber(310, 630)), new Vector2(RandomNumber(-2, -1), RandomNumber(0, 0))));
                            sprites.Sprites.Add(createAsteroid01(new Vector2(RandomNumber(2400, 2920), RandomNumber(110, 610)), new Vector2(RandomNumber(-2, -1), RandomNumber(0, 0))));
                        }
                    }

                    if (currentscore > 2000 && currentscore < 3999) // Medium speed 
                    {
                        if (spriteelapsed % 1400 == 0)              // Add every 1.4 seconds
                        {
                            sprites.Sprites.Add(createAsteroid01(new Vector2(RandomNumber(1300, 1420), RandomNumber(100, 420)), new Vector2(RandomNumber(-4, -2), RandomNumber(0, 0))));
                            sprites.Sprites.Add(createAsteroid01(new Vector2(RandomNumber(1800, 2020), RandomNumber(310, 630)), new Vector2(RandomNumber(-4, -2), RandomNumber(0, 0))));
                            sprites.Sprites.Add(createAsteroid01(new Vector2(RandomNumber(2400, 2920), RandomNumber(110, 610)), new Vector2(RandomNumber(-4, -2), RandomNumber(0, 0))));
                        }
                    }

                    if (currentscore > 4000 && currentscore < 5999) // High speed
                    {
                        if (spriteelapsed % 1500 == 0)               // Add every 1.5 seconds
                        {
                            sprites.Sprites.Add(createAsteroid01(new Vector2(RandomNumber(1300, 1420), RandomNumber(100, 420)), new Vector2(RandomNumber(-6, -4), RandomNumber(0, 0))));
                            sprites.Sprites.Add(createAsteroid01(new Vector2(RandomNumber(1800, 2020), RandomNumber(310, 630)), new Vector2(RandomNumber(-6, -4), RandomNumber(0, 0))));
                            sprites.Sprites.Add(createAsteroid01(new Vector2(RandomNumber(2400, 2920), RandomNumber(110, 610)), new Vector2(RandomNumber(-6, -4), RandomNumber(0, 0))));
                        }
                    }

                    if (currentscore > 6000)                        // insane speed
                    {
                        if (spriteelapsed % 1200 == 0)               // Add every 1.2 seconds
                        {
                            sprites.Sprites.Add(createAsteroid01(new Vector2(RandomNumber(1300, 1420), RandomNumber(100, 420)), new Vector2(RandomNumber(-8, -6), RandomNumber(0, 0))));
                            sprites.Sprites.Add(createAsteroid01(new Vector2(RandomNumber(1800, 2020), RandomNumber(310, 630)), new Vector2(RandomNumber(-8, -6), RandomNumber(0, 0))));
                            sprites.Sprites.Add(createAsteroid01(new Vector2(RandomNumber(2400, 2920), RandomNumber(110, 610)), new Vector2(RandomNumber(-8, -6), RandomNumber(0, 0))));
                        }
                    }
                }

                // # # # # # # # # # # # # # # # # # # # # # # # # #
                // Play sound on screen collide
                // # # # # # # # # # # # # # # # # # # # # # # # # #
                if (UserControlledSprite.screencrashcheck == 1)                 // if player == hit screen
                {
                    playscreensound = true;
                    UserControlledSprite.screencrashcheck = 0;
                }
                else if (UserControlledSprite.screencrashcheck == 0)            // if player != hit screen
                {
                    playscreensound = false;
                }

                // # # # # # # # # # # # # # # # # # # # # # # # # #
                // Player loses all lives
                // # # # # # # # # # # # # # # # # # # # # # # # # #
                if (CurrentLives == 0)
                {
                    backgroundI.Stop(); shoot1I.Stop();
                    ScreenManager.AddScreen(new JoshDemoFinish());
                    Remove(); cleanUp();
                }

                // # # # # # # # # # # # # # # # # # # # # # # # # #
                // Updates all sprites as well as the game
                // # # # # # # # # # # # # # # # # # # # # # # # # #
                foreach (AnimatedSprite d in sprites.Sprites)
                {
                    d.Update(gameTime, viewportRect);                           // updates all sprites in list
                    if (sprites.Player.BoundingBox.Intersects(d.BoundingBox))
                    {
                        CurrentLives--;
                        d.screenPos.X = d.screenPos.X +1500;
                        shoot1I.Play();
                    }
                }
                sprites.Player.Update(gameTime, viewportRect);
                base.Update(gameTime, covered);

                // # # # # # # # # # # # # # # # # # # # # # # # # #
                // If escape is pressed (Pause/Unpause)
                // # # # # # # # # # # # # # # # # # # # # # # # # #
                if (TotalElapsedTime >= KeyPressCheckDelay)
                {
                    if (input.MenuCancel)                                       // if escape is pressed
                    {
                        GameState = GameStates.Paused;                          // pause game
                        TotalElapsedTime = 0.0f;
                        audioToggle();
                        ScreenManager.AddScreen(new LevelMenu());       // display ingame menu
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
                        audioToggle();
                    }
                }
            }
        }

        #region Audio
        //Pause/Play
        public void audioToggle()
    {
        if (backgroundI.State == SoundState.Stopped)
        {
            backgroundI.Volume = globalVolume;
            backgroundI.Play();
        }
        else if (backgroundI.State == SoundState.Playing)
        {
            backgroundI.Pause();
        }
        else if (backgroundI.State == SoundState.Paused)
        {
            backgroundI.Resume();
        }
    }
        //Volume Increse
        public void audioVolumeIncrease()
    {
        if(backgroundI.Volume != 1.0f)
        {
            backgroundI.Volume += 0.1f;
        }
        if (shoot1I.Volume != 1.0f)
        {
            shoot1I.Volume += 0.1f;
        }
        if (shoot2I.Volume != 1.0f)
        {
            shoot2I.Volume += 0.1f;
        }
        if (globalVolume != 1.0f)
        {
            globalVolume += 0.1f;
        } 
    }
        //Volume Decrease
        public void audioVolumeDecrease()
    {
        if(backgroundI.Volume > 0.0f)
        {
            backgroundI.Volume -= 0.1f;
        }
        if (shoot1I.Volume > 0.0f)
        {
            shoot1I.Volume -= 0.1f;
        }
        if (shoot2I.Volume > 0.0f)
        {
            shoot2I.Volume -= 0.1f;
        }
        if (globalVolume > 0.0f)
        {
            globalVolume -= 0.1f;
        }
    }
        #endregion

        public override void Remove() { base.Remove(); }

        public void PlaySounds()
        {
            //Play colission sound
            if (playscreensound == true)
            {
                shoot1I.Volume = globalVolume;
                shoot1I.Play();
            }
        }

        public void DrawText()
        {
            // # # # # # # # # # # # # # # # # # # # # # # # # #
            // Sets up the XML files for reading
            // # # # # # # # # # # # # # # # # # # # # # # # # #
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            System.Xml.XmlDocument keyConfigXML = new System.Xml.XmlDocument();
            keyConfigXML.Load(Game._path + "\\Content\\KeybindingsSettings.xml");

            spriteBatch.DrawString(font1, "Josh's Testing", new Vector2(10, 10), Color.Pink);
            spriteBatch.DrawString(font2, "Playing", new Vector2(1150, 10), Color.Pink);
            // SHAUNS EDIT: Changed your text to actually read the value of the desired keys // Thanks Shaun :D!
            spriteBatch.DrawString(font1, "Lives: " + CurrentLives, new Vector2(520, 10), Color.White);
            spriteBatch.DrawString(font1, "Score: " + currentscore, new Vector2(650, 10), Color.White);
            spriteBatch.DrawString(font2, "" + Convert.ToString(keyConfigXML.SelectSingleNode("//KBAudioToggle").InnerText) + " - Pause \n" + Convert.ToString(keyConfigXML.SelectSingleNode("//KBAudioVolumeUp").InnerText) + " - VolUp \n" + Convert.ToString(keyConfigXML.SelectSingleNode("//KBAudioVolumeDown").InnerText) + " - VolDown", new Vector2(10, 40), Color.Pink);
        }

        public override void Draw(GameTime gameTime)
        {
            //Reloads the spritebatch
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Resolution.BeginDraw();

            foreach (Layer layer in _layers) { layer.Draw(spriteBatch); }

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());
            DrawText();
            PlaySounds();
            foreach (AnimatedSprite d in sprites.Sprites) { d.Draw(gameTime, ScreenManager.SpriteBatch, Color.White); }
            sprites.Player.Draw(gameTime, spriteBatch, Color.White);
            spriteBatch.End();
        }

        private void cleanUp() { fonts.clearManager(); textures.clearManager(); sounds.clearManager(); sprites.Sprites.Clear(); }
    }
}