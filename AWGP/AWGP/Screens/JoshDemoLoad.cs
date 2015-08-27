/*
 * Author:          Shaun Taylor
 * Description:     Loading splash screen for Josh's Demo
 * Last Updated:    03/01/2013
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

namespace AWGP
{
    public class JoshDemoLoad : SplashScreen
    {
        public JoshDemoLoad()
        {
            ScreenTime = TimeSpan.FromSeconds(5);
            OpacityColor = Color.White; Opacity = 0.9f;
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            BackgroundTexture = content.Load<Texture2D>("Textures\\avoidance\\avoidanceloadscreen");
            Pixel = content.Load<Texture2D>("Textures\\pixel");
        }

        public override void Remove() { ScreenManager.AddScreen(new JoshDemo()); base.Remove(); }
    }
}
