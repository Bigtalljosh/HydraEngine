/*
 * Author:          Shaun Taylor
 * Description:     Objectives/story splash screen for Josh's Demo
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
    public class JoshDemoStory : SplashScreen
    {
        public JoshDemoStory()
        {
            ScreenTime = TimeSpan.FromSeconds(30); TransitionOnTime = TimeSpan.FromSeconds(0); TransitionOffTime = TimeSpan.FromSeconds(0);
            OpacityColor = Color.White; Opacity = 0.9f;
        }

        public override void HandleInput()
        {
            InputManager input = ScreenManager.InputSystem;
            if (input.MenuSelect) { Remove(); }
            base.HandleInput();
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            BackgroundTexture = content.Load<Texture2D>("Textures\\avoidance\\avoidancestoryscreen");
            Pixel = content.Load<Texture2D>("Textures\\pixel");
        }

        public override void Remove() { base.Remove(); ScreenManager.AddScreen(new JoshDemoLoad()); }
    }
}
