using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Shard
{
    class Debris : ShardObject
    {
        private int health;

        public Debris() : this(0, 0) { }

        public Debris(int xPosition, int yPosition)
            : base(xPosition, yPosition)
        {
            health = 1;
        }

        public virtual int Health
        {
            get
            {
                return this.health;
            }
            set
            {
                this.health = value;
            }
        }

    }
}
