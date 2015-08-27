/* ## RECENT MESSAGES ##
 * (From Shaun)(22/12/2012)> The application now autoscales, in the draw method you want to include; Resolution.BeginDraw(); then in the spriteBatch
 * call have this; spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());
 * You can have numerous spriteBatch.Begin calls remember allowing you to have content that isn't autoscaled based on current resolution
 * not sure why you would do this, but it's there if you want it. So want autoscaled? In the modified .Begin call.. don't? Stick it in a second one.
 * 
 * (From Shaun)(24/12/2012)> Input manager set up, take a look at KeybindingsSettings.xml + KeybindingsConfig.cs + InputManager.cs + Demo01.cs
 * to see how it works, remember speak to me (Shaun) if you get stuck, takes some work to get things going to begin with, but allows us to
 * edit the keybindings from the XML file which should get us marks?
 * 
 * (From Shaun)(24/12/2012)> When calling the spritebatch.draw --> try using this:  ScreenManager.Spritebatch.Draw instead should bypass you having to reload it
 * each time you want to use it in a method.. not sure if it will fully work.. but at least we find out if you use it.. Remember using this in the
 * update method like you were taught last year usually ends with an error as Update is called before your Draw method where it begin's and ends.
 * 
 * (From Shaun)(25/12/2012)> Yes.. Working on Xmas.. i'm sad.. Anyway. I'm starting to tidy everything up, i'm making sure everything is clearly commented on
 * stuff I can't see changing any more. Then adding the Author to the top of the class. Be sure to do the same for anything you guys may own. Remember
 * the better you comment things, the easier it will be during our meeting to explain what everything does and how it works.
 * 
 * (From Josh)(30/12/2012)> Sound works, CHeck my class out to see how it's simple enough. Looking into making an EffectManager much like the other ones.
 * I'll get started on the Report tomorrow. I'm guessing Bens not doing anything other than the copy/paste which from Reimers, which you then had to fix.
 * (Leave this comment in here for the record) A problem with the logic at current that determins the boundries in the sprite classes. I guess that's due
 * to the way it auto scales now, it works fine in my other project. Also, need to find a way to flag from the sprite class to the demo class that it needs 
 * to play the colission noise. WIll Also start commenting everything nicely tomorrow.
 * #KNOWN BUGS#
 * When you exit my demo screen and reload it, I've made it so it tells you the error and continues, rather than stopping the application.
 * Basically because we're only using the same ContentManager throughout, I can't use the .Dispose() method or it unloads EVERYTHING that ContentManager 
 * has loaded, so then the whole program dies. This way, it just tells you it's not gonna re-load them because they already exist.
 */

/* ## KNOWN BUGS ##
 * (From Shaun)> Auto-resolution scale doesn't account for mouse scaling, therefore at the moment if you try and mouse over the cube in the demo01.cs
 * then it won't actually highlight.. Having an issue getting the matrix working for mouse input worst comes to worst, we just demo at 1280x720 but show
 * briefly that the application does auto-scale.
 */

/* ## THINGS TO DO ##
 * Shaun> Polishing up the screen-manager get audio in basically until an audio manager is working. Get auto-resolution working 
 *        for mouse input as well somehow, as well as a user-manager to load/save settings based on who is logged in to the
 *        system. Beautify the Interaction demo, at least make it aesthetically pleasing.
 * Josh>  Font, Texture and Audio Manger. Get a demo working that show-cases all of these effectively? Not really sure of a 
 *        decent way of displaying these? 
 * Ben>   Trololo, no offence but what the actual fuck.. Please for the love of god get a working Physics demo working, don't just
 *        rip of a tutorial and think that's it. Take the tutorial, expand upon it and try to make it your own. We have to show
 *        how other people can use the physics if they wanted to. Remember, the plan for me would to be able to turn off the
 *        physics via settings and the demo still work.. just instead of working things out in physics terms, it's basic maths.
 */

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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Reflection;
// Allows the application to use the second project within the solution.
using SharedContent;
using AWGP.Input;

namespace AWGP
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Different components..
        ScreenManager screenManager;
        InputManager inputManager;


        // These are global variables which are used numerous times throughout the application, therefore it makes
        // perfect sense to have them defined once then you access them by using Game._Path or Game._Index
        public static int _Index;
        public static int _index { get { return _Index; } set { _Index = value; } }
        public static String _Path;
        public static String _path { get { return _Path; } set { _Path = value; } }
        public static KeybindingsConfig KeyBindings;
        public static KeybindingsConfig keyBindings { get { return KeyBindings; } set { KeyBindings = value; } }

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            // Initialises the Resolution class, allowing the game to auto-scale all assets based on the resolution.
            Resolution.Init(ref graphics);
            Content.RootDirectory = "Content";

            // Sets up the path to the content folder based on the .exe local location
            _index = Assembly.GetExecutingAssembly().Location.LastIndexOf("\\");
            _path = Assembly.GetExecutingAssembly().Location.Substring(0, _index);

            // Loads the Application Settings XML file
            System.Xml.XmlDocument appConfigXML = new System.Xml.XmlDocument();
            System.Xml.XmlDocument serConfigXML = new System.Xml.XmlDocument();
            appConfigXML.Load(Game._path + "\\Content\\ApplicationSettings.xml");
            serConfigXML.Load(Game._path + "\\Content\\ServiceSettings.xml");

            // Sets the application settings based on the values of the XML file.
            // Some of the values have to be converted to a different type as when they are read
            // they are all read in as Strings. Of course this then doesn't match the intended type.
            Window.Title = appConfigXML.SelectSingleNode("//ScreenTitle").InnerText;
            this.IsMouseVisible = Convert.ToBoolean(appConfigXML.SelectSingleNode("//MouseActive").InnerText);
            int selectedResolutionWidth = Convert.ToInt16(appConfigXML.SelectSingleNode("//ScreenWidth").InnerText);
            int selectedResolutionHeight = Convert.ToInt16(appConfigXML.SelectSingleNode("//ScreenHeight").InnerText);
            bool selectedFullScreen = Convert.ToBoolean(appConfigXML.SelectSingleNode("//FullScreen").InnerText);

            // Change Virtual Resolution 
            Resolution.SetVirtualResolution(1280, 720); // This is the default resolution.. do not change this or you'll break everything!
            Resolution.SetResolution(selectedResolutionWidth, selectedResolutionHeight, selectedFullScreen);

            // Loads the screenmanager
            // Ideally we would want all of our stuff to plug in like this. i.e.
            // Components.Add(userManager); Components.Add(resourceManager); Components.Add(physicsManager); etc
            screenManager = new ScreenManager(this);
            inputManager = new InputManager();
            Components.Add(screenManager);
        }

        protected override void Initialize()
        {
            // Adds the first screen of the application to the stack
            /*
             * 
             *  To Shaun, from Shaun:
             *  Instead of starting from a splashscreen, have a userloginscreen(), which will be used to load
             *  user specific application settings. Make a varient of the blue screen for easy visual notification
             *  of different users. The splash screens can then be used as loading time to load everything for the 
             *  user.
             * 
             * For the time being to save time, I am just loading the ControllerSelectScreen as I am too lazy to
             * wait for all the screens to pass. Remember to change this before handing in! 
             * 
             */
            screenManager.AddScreen(new EngineSplash());
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }

        void RecieveInput(PlayerIndex index, String action)
        {

        }
    }
}