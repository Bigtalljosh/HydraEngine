/*
 * Author:          Shaun Taylor
 * Description:     Sets up the layers and allows the drawing of sprites for the parallax
 *                  scrolling examples.
 * Last Updated:    06/01/2013
 * Progress:        100% Complete
 * Credits:         http://www.david-gouveia.com/2d-camera-with-parallax-scrolling-in-xna/
 * 
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AWGP
{
    public class Layer
    {
        public Layer(Camera camera)
        {
            _camera = camera;
            Parallax = Vector2.One;
            Sprites = new List<Sprite2>();
        }

        public Vector2 Parallax { get; set; }

        public List<Sprite2> Sprites { get; private set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _camera.GetViewMatrix(Parallax));

            foreach (Sprite2 sprite in Sprites)
                sprite.Draw(spriteBatch);

            spriteBatch.End();
        }

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, _camera.GetViewMatrix(Parallax));
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(_camera.GetViewMatrix(Parallax)));
        }

        private readonly Camera _camera;
    }
}
