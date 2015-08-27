/*
 * Author:          Shaun Taylor
 * Description:     Sets up the camera for the viewport, allowing you to zoom in and out as well as 
 *                  rotate the assets within the layers.
 * Last Updated:    06/01/2013
 * Progress:        100% Complete
 * Credits:         http://www.david-gouveia.com/2d-camera-with-parallax-scrolling-in-xna/
 * 
 */

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AWGP
{
    public class SimpleCamera
    {
        public SimpleCamera(Viewport viewport)
        {
            Origin = new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);
            Zoom = 1.0f;
        }

        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public float Zoom { get; set; }
        public float Rotation { get; set; }

        public Matrix GetViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-Position * parallax, 0.0f)) *
                   Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                   Matrix.CreateRotationZ(Rotation) *
                   Matrix.CreateScale(Zoom) *
                   Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }
    }
}
