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
        private int damage; //The Damage the bullet will attempt to deal on collision with a damagable object

        public Projectile() : this(0,0)
        { }

        public Projectile(int xPosition, int yPosition) : base(xPosition, yPosition)
        {
            this.damage = 1;
            Health = 200; //Projectiles use Health as a life timer, when it reaches 0 they are marked invalid
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

        public override void Update(List<ShardObject> shardObjects, GameTime gameTime)
        {
            base.Update(shardObjects, gameTime);
            foreach (ShardObject shardObject in shardObjects)
            {
                if (shardObject is Debris)
                {
                    if (GetBounds().Intersects(shardObject.GetBounds()))
                    {
                        shardObject.Health -= Damage;
                        this.Destroy(shardObjects);
                    }
                }
            }
            Health -= 1;
        }

    }
}
