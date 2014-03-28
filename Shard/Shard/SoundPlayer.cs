using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Shard
{
    class SoundPlayer
    {

        private Dictionary<string, SoundEffect> sounds;

        public SoundPlayer()
        {
            sounds = new Dictionary<string, SoundEffect>();
        }

        public void addSound(string key, SoundEffect sfx)
        {
            sounds.Add(key, sfx);
        }

        public SoundEffect getSound(string key)
        {
            return sounds[key];
        }

        public void LoadSounds()
        {
            //stuff will happen here eventually
            //...
            //...
            //I promise
        }

    }
}
