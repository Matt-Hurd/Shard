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
    class Projectile : ShardObject
    {
        private int damage; //Duh

        public Projectile() : this(0,0)
        { }

        public Projectile(int xPosition, int yPosition) : base(xPosition, yPosition)
        {
            this.damage = 1;
        }

        public virtual int Damage
        {
            get
            {
                return this.damage;
            }
            set
            {
                this.damage = value;
            }
        }

    }
}
