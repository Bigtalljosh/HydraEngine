//Author: Josh
//This is meant to encapsulate each manager into one main manager. Some reason I had a problem with the contentmanager always being null and I was unable to set it.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace AWGP
{
    public class ResourceManager
    {
        public ContentManager ContentManager { get; set; }
        private TextureManager textures = TextureManager.Instance;
        private SoundManager sounds = SoundManager.Instance;
        private FontManager fonts = FontManager.Instance;

        private static ResourceManager instance;
        public static ResourceManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ResourceManager();
                return instance;
            }
        }

        public ResourceManager()
        {
            textures.ContentManager = this.ContentManager;
            fonts.ContentManager = this.ContentManager;
            //textures.ContentManager = ScreenManager.Game.Content;
            //sounds.ContentManager = ScreenManager.Game.Content;
            //fonts.ContentManager = ScreenManager.Game.Content;
        }

        #region Loads
        public void loadFont(string key, string path)
        {
            fonts.loadFont(key, path);
        }

        public void loadTexture(string key, string path)
        {
            textures.LoadTexture(key, path);
        }

        public void loadSoundEffect(string key, string path)
        {
            sounds.LoadSoundEffect(key, path);
        }

        public void loadSoundEffectInstance(string key, string path)
        {
            sounds.LoadSoundEffectInstance(key, path);
        }

        #endregion

        #region gets
        public SpriteFont GetFontByKey(string key)
        {
            return fonts.GetFontByKey(key);
        }

        public SoundEffect GetSoundEffectByKey(string key)
        {
            return sounds.GetSoundEffectByKey(key);
        }

        public SoundEffectInstance GetSoundEffectInstanceByKey(string key)
        {
            return sounds.GetSoundEffectInstanceByKey(key);
        }

        public Texture2D GetTextureByKey(string key)
        {
            return textures.GetTextureByKey(key);
        }

        #endregion

        #region removes

        public bool RemoveFontByKey(string key)
        {
            return fonts.RemoveFontByKey(key);
        }

        public bool RemoveTextureByKey(string key)
        {
            return textures.RemoveTextureByKey(key);
        }

        public bool RemoveSoundEffectInstnaceByKey(string key)
        {
            return sounds.RemoveSoundEffectInstnaceByKey(key);
        }

        public bool RemoveSoundEffectByKey(string key)
        {
            return sounds.RemoveSoundEffectByKey(key);
        }

        #endregion

    }
}

