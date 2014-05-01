using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace Shard
{
    public class SoundPlayer
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

        public void LoadSounds(ContentManager Content)
        {
            SoundEffect sfx;
            sounds.Add("playerShoot", sfx = Content.Load<SoundEffect>("Sounds/playerShoot"));
            sounds.Add("playerMissile", sfx = Content.Load<SoundEffect>("Sounds/playerMissile"));
            sounds.Add("enemyShoot", sfx = Content.Load<SoundEffect>("Sounds/enemyShoot"));
            sounds.Add("enemyMissile", sfx = Content.Load<SoundEffect>("Sounds/enemyMissile"));

        }
    }
}
