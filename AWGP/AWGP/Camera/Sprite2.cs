/*
 * Author:          Shaun Taylor
 * Description:     Sets up a instance of another sprite, specifically for the use for 
 *                  oarallax scrolling.
 * Last Updated:    06/01/2013
 * Progress:        100% Complete
 * Credits:         http://www.david-gouveia.com/2d-camera-with-parallax-scrolling-in-xna/
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AWGP
{
    public struct Sprite2
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Rotation;

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
                spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
