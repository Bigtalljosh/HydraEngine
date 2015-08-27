﻿/*
 * Author:          Shaun Taylor
 * Description:     Displays the university logo at the start of the application
 * Last Updated:    06/01/2013
 * Progress:        100% Complete
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
// Allows the application to use the second project within the solution.
using SharedContent;

namespace AWGP
{
    public class UniversitySplash : SplashScreen
    {
        ScreensConfig scrConfig;

        public UniversitySplash()
        {

        }

        public override void LoadContent()
        {
            // Reload the content manager and loads up the ScreensSettings.xml for reading
            ContentManager Content = ScreenManager.Game.Content;
            scrConfig = Content.Load<ScreensConfig>("ScreensSettings");

            // Load screen parameters from the ScreensSettings.xml
            OpacityColor = Color.White;         // This can't be editable without writing a StringToColor database?
            ScreenTime = TimeSpan.FromSeconds(scrConfig.UniversitySplash_Duration);
            Opacity = scrConfig.UniversitySplash_Opacity;

            // Load the images for the background image and transition from ScreensSettings.xml
            BackgroundTexture = Content.Load<Texture2D>(scrConfig.UniversitySplash_BGImage);
            Pixel = Content.Load<Texture2D>(scrConfig.Transition_BGImage);
        }
        public override void Remove()
        {
            // After the ScreenTime variable counts to 0, loads the next screen then removes current from stack.
            ScreenManager.AddScreen(new ControllerSelectScreen());
            base.Remove();
        }
    }
}
