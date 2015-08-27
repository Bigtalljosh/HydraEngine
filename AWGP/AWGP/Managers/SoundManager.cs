/* Author - Josh
 * This class manages the loading of Sound and the returning a link to the Sound in the list, and also the removing of a Sound
 * It also has MP3 support, using the Song object type.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

namespace AWGP
{
    public class SoundManager
    {
        public ContentManager ContentManager { get; set; }
        //Creates a Dictionary in order to link a string to a specific Sound, soundEffects, or Songs
        readonly Dictionary<string, SoundEffect> SoundEffectDictionary;
        readonly Dictionary<string, SoundEffectInstance> SoundEffectInstnaceDictionary;
        readonly Dictionary<string, Song> SongDictionary;
        readonly Dictionary<string, AudioEmitter> EmitterDictionary;
        readonly Dictionary<string, AudioListener> ListenerDictionary;
        int count;

        //Allows only a single instance of the manager to be created at any one time.
        private static SoundManager instance;
        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SoundManager();
                return instance;
            }
        }

        private SoundManager()
        {
            //Creates the dictionary structure in memory
            //At the minute only allows 20 Sounds/soundeffectinstances/songs to be stored, Need to update to be dynamic and grow as resources are added
            SoundEffectDictionary = new Dictionary<string, SoundEffect>(20);
            SoundEffectInstnaceDictionary = new Dictionary<string, SoundEffectInstance>(20);
            SongDictionary = new Dictionary<string, Song>(20);
            EmitterDictionary = new Dictionary<string, AudioEmitter>(20);
            ListenerDictionary = new Dictionary<string, AudioListener>(4);
            count = 0;
        }

        #region SoundEffects
        //Loads the sound in using the content manager, gives errors if the key is already in use(It must be unique) or if the specific sound is already in the dictionary
        public void LoadSoundEffect(string key, string path) //Need validation code
        {
            if (ContentManager != null)
            {
                if (SoundEffectDictionary.ContainsKey(key))
                {
                    //MessageBox.Show("Key already in use!" + "'" + key + "'");
                }

                else if (SoundEffectDictionary.ContainsValue(ContentManager.Load<SoundEffect>(path)))
                {
                    //MessageBox.Show("SoundEffect already loaded!" + "'" + path + "'");
                }
                else
                {
                    SoundEffectDictionary.Add(key, ContentManager.Load<SoundEffect>(path));
                }
            }
            else
            {
                throw new ArgumentNullException("Content");
            }
        }

        //Returns a reference to the soundeffect in the dictionary structure
        public SoundEffect GetSoundEffectByKey(string key)
        {
            SoundEffect sfx = SoundEffectDictionary[key];
            return sfx;
        }

        //removes the texture from the dictionary
        public bool RemoveSoundEffectByKey(string key)
        {
            return SoundEffectDictionary.Remove(key);
        }

        public SoundEffect this[string key]
        {
            get
            {
                return SoundEffectDictionary[key];
            }
        }
        #endregion

        #region SoundEffectInstances
        public void LoadSoundEffectInstance(string key, string path) //Need validation code
        {
            if (ContentManager != null)
            {
                if (SoundEffectInstnaceDictionary.ContainsKey(key))
                {
                    MessageBox.Show("Key already in use!");
                }

                else if (SoundEffectInstnaceDictionary.ContainsValue(ContentManager.Load<SoundEffectInstance>(path)))
                {
                    MessageBox.Show("SoundEffect already loaded!");
                }
                else
                {
                    SoundEffectInstnaceDictionary.Add(key, ContentManager.Load<SoundEffectInstance>(path));
                }
            }
            else
            {
                throw new ArgumentNullException("Content");
            }
        }

        public SoundEffectInstance GetSoundEffectInstanceByKey(string key)
        {
            SoundEffectInstance sfxI = SoundEffectInstnaceDictionary[key];
            return sfxI;
        }

        public bool RemoveSoundEffectInstnaceByKey(string key)
        {
            return SoundEffectInstnaceDictionary.Remove(key);
        }

        #endregion

        #region Songs
        public void LoadSong(string key, string path) //Need validation code
        {
            if (ContentManager != null)
            {
                if (SoundEffectInstnaceDictionary.ContainsKey(key))
                {
                    MessageBox.Show("Key already in use!");
                }

                else if (SongDictionary.ContainsValue(ContentManager.Load<Song>(path)))
                {
                    MessageBox.Show("Song already loaded!");
                }
                else
                {
                    SongDictionary.Add(key, ContentManager.Load<Song>(path));
                }
            }
            else
            {
                throw new ArgumentNullException("Content");
            }
        }

        public Song GetSongByKey(string key)
        {
            Song song = SongDictionary[key];
            return song;
        }

        public bool RemoveSongByKey(string key)
        {
            return SongDictionary.Remove(key);
        }
        #endregion

        #region Emitters
        public void CreateEmitter(string key)
        {
            AudioEmitter emitter = new AudioEmitter();
            EmitterDictionary.Add(key, emitter);
        }

        public AudioEmitter GetEmitterByKey(string key)
        {
            AudioEmitter emt = EmitterDictionary[key];
            return emt;
        }

        public void SetEmitterPosition(string key, Vector2 pos)
        {
            AudioEmitter temp = GetEmitterByKey(key);
            Vector3 result;
            result.X = pos.X;
            result.Y = pos.Y;
            result.Z = 0.0f;
            temp.Position = result;
            EmitterDictionary[key] = temp;
        }

        public Vector2 getEmitterPosition(string key)
        {
            AudioEmitter temp = GetEmitterByKey(key);
            Vector2 result;
            result.X = temp.Position.X;
            result.Y = temp.Position.Y;

            return result;
        }

        public bool RemoveEmitterEffectByKey(string key)
        {
            return EmitterDictionary.Remove(key);
        }

        #endregion

        #region Listeners
        public void CreateListener(string key)
        {
            if (count < 5)
            {
                AudioListener listener = new AudioListener();
                ListenerDictionary.Add(key, listener);
            }
            else
            {
                MessageBox.Show("Cannot have more than 4 listeners, 4 is max players");
            }
        }

        public AudioListener GetListenerByKey(string key)
        {
            AudioListener lis = ListenerDictionary[key];
            return lis;
        }

        public void SetListenerPosition(string key, Vector2 pos)
        {
            AudioListener temp = GetListenerByKey(key);
            Vector3 result;
            result.X = pos.X;
            result.Y = pos.Y;
            result.Z = 0.0f;
            temp.Position = result;
            ListenerDictionary[key] = temp;
        }

        public Vector2 getListenerPosition(string key)
        {
            AudioListener temp = GetListenerByKey(key);
            Vector2 result;
            result.X = temp.Position.X;
            result.Y = temp.Position.Y;

            return result;
        }

        public bool RemoveListenerByKey(string key)
        {
            return ListenerDictionary.Remove(key);
        }
        #endregion

        public void clearManager()
        {
            SoundEffectDictionary.Clear();
            SoundEffectInstnaceDictionary.Clear();
            SongDictionary.Clear();
            ListenerDictionary.Clear();
            EmitterDictionary.Clear();
        }
    }
}
