//Author: Josh
//Handles the creation, and management of sprites, player or AI sprites.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AWGP.Graphics.Sprites;

namespace AWGP
{
    public class SpriteManager
    {
        private static SpriteManager spriteManager;
        public static SpriteManager Instance
        {
            get
            {
                if (spriteManager == null)
                    spriteManager = new SpriteManager();
                return spriteManager;
            }
        }

        protected SpriteManager()
        {
            initialiseSpriteManager(20);
        }

        public SpriteManager(int initialCapacity)
        {
            initialiseSpriteManager(initialCapacity);
        }

        public Game Game { get; set; }

        private void initialiseSpriteManager(int initialCapacity)
        {
            sprites = new List<AnimatedSprite>(initialCapacity);
        }

        public UserControlledSprite Player { get; set; }
        
        private List<AnimatedSprite> sprites;
        public List<AnimatedSprite> Sprites
        {
            get
            {
                return sprites;
            }
        }
    }
}
