/* Author - Josh
 * This class manages the loading of Textures and the returning a link to the Texture in the list, and also the removing of a Texture
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
    public class TextureManager
    {
        public ContentManager ContentManager { get; set; }
        //Creates a Dictionary in order to link a string to a specific Texture
        readonly Dictionary<string, Texture2D> textureDictionary;

        //Allows only a single instance of the manager to be created at any one time.
        private static TextureManager instance;
        public static TextureManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new TextureManager();
                return instance;
            }
        }

        private TextureManager()
        {
            //Creates the dictionary structure in memory
            //At the minute only allows 20 textures to be stored, Need to update to be dynamic and grow as textures are added
            textureDictionary = new Dictionary<string, Texture2D>(20); 
        }

        //Loads the texture in using the content manager, gives errors if the key is already in use(It must be unique) or if the specific texture is already in the dictionary
        public void LoadTexture(string key, string path)
        {
            if (ContentManager != null)
            {
                if (textureDictionary.ContainsKey(key))
                {
                    //MessageBox.Show("Key already in use!" + "'" + key + "'");
                }

                else if (textureDictionary.ContainsValue(ContentManager.Load<Texture2D>(path)))
                {
                    //MessageBox.Show("Texture already loaded!" + "'" + path + "'");
                    
                }
                else
                {
                    textureDictionary.Add(key, ContentManager.Load<Texture2D>(path));
                }
            }
            else
            {
                MessageBox.Show("Content Manager not loaded");
                throw new ArgumentNullException("ContentManager");
            }
        }

        //Returns a reference to the texture in the dictionary structure
        public Texture2D GetTextureByKey(string key)
        {
            Texture2D tex = textureDictionary[key];
            return tex;
        }

        //removes the texture from the dictionary
        public bool RemoveTextureByKey(string key)
        {
            return textureDictionary.Remove(key);
        }

        public Texture2D this[string key]
        {
            get
            {
                return textureDictionary[key];
            }
        }

        public void clearManager()
        {
            textureDictionary.Clear();
        }
    }
}