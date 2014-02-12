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
        private Point target; //The location that this projectile is attempting to reach using "dumb motion", if null then just obey velocity

        public Projectile() : this(0,0)
        { }

        public Projectile(int xPosition, int yPosition) : base(xPosition, yPosition)
        {
            this.damage = 1;
            this.target = new Point(-1,-1); ;
        }

    }
}
