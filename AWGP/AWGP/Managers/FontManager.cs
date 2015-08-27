/* Author - Josh
 * This class manages the loading of fonts and the returning a link to the font in the list, and also the removing of a font
 */ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AWGP 
{
    public class FontManager
    {
        public ContentManager ContentManager { get; set; }
        //Creates a Dictionary in order to link a string to a specific font
        readonly Dictionary<string, SpriteFont> FontDictionary;

        //Allows only a single instance of the manager to be created at any one time.
        private static FontManager instance;
        public static FontManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new FontManager();
                return instance;
            }
        }

        private FontManager()
        {
            //Creates the dictionary structure in memory
            //At the minute only allows 20 fonts to be stored, Need to update to be dynamic and grow as resources are added
            FontDictionary = new Dictionary<string, SpriteFont>(20);
        }

        //Loads the font in using the content manager, gives errors if the key is already in use(It must be unique) or if the specific sound is already in the dictionary
        public void loadFont(string key, string path)
        {
            if (ContentManager != null)
            {
                if (FontDictionary.ContainsKey(key))
                {
                    MessageBox.Show("Key already in use!" + "'" + key + "'");
                }

                else if (FontDictionary.ContainsValue(ContentManager.Load<SpriteFont>(path)))
                {
                    MessageBox.Show("Font already loaded!" + "'" + path + "'");
                }
                else
                {
                    FontDictionary.Add(key, ContentManager.Load<SpriteFont>(path));
                }
            }
            else
                throw new ArgumentNullException("Content");
        }

        //Returns a reference to the font in the dictionary structure
        public SpriteFont GetFontByKey(string key)
        {
            SpriteFont font = FontDictionary[key];
            return font;
        }

        //removes the texture from the dictionary
        public bool RemoveFontByKey(string key)
        {
            return FontDictionary.Remove(key);
        }

        public SpriteFont this[string key]
        {
            get
            {
                return FontDictionary[key];
            }
        }

        public void clearManager()
        {
            FontDictionary.Clear();
        }
    }
}
