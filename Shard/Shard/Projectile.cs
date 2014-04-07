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
        private int currentLifetime;
        private int maxLifetime;

        public Projectile()
            : this(0, 0)
        { }

        public Projectile(int xPosition, int yPosition)
            : base(xPosition, yPosition)
        {
            this.damage = 1;
            currentLifetime = 0; //Projectiles use a life timer, when it reaches maxLifetime they are marked invalid
            maxLifetime = 125;
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

        public int CurrentLife
        {
            get
            {
                return this.currentLifetime;
            }
            set
            {
                this.currentLifetime = value;
            }
        }

        public int MaximumLifetime
        {
            get
            {
                return this.maxLifetime;
            }
            set
            {
                this.maxLifetime = value;
            }
        }

        /*
         * precondition: A ShardObject that collides with this projectile
         */
        public virtual void HandleCollision(ShardObject shardObject)
        {
            shardObject.ApplyDamage(damage);
            this.SetValid(false);
        }

        public override void Update(List<ShardObject> shardObjects, GameTime gameTime)
        {
            base.Update(shardObjects, gameTime);
            if (currentLifetime >= maxLifetime)
                SetValid(false);

            for (int i = 0; i < shardObjects.Count; i++ )
            {
                ShardObject shardObject = shardObjects[i];
                if (!(shardObject is Projectile) && shardObject.Solid && shardObject.Alignment != this.Alignment)
                {
                    if (GetBounds().Intersects(shardObject.GetBounds()))
                    {
                        HandleCollision(shardObject);
                    }
                }
            }

            currentLifetime++;
        }

    }
}
