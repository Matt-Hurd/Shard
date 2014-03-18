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
    class Ship : ShardObject
    {
        //int speedLevel;
        //int armorLevel;
        int gunLevel;
        //int missileLevel;
        //int laserLevel;

        public Ship(int xPosition, int yPosition)
        {
            this.X = xPosition;
            this.Y = yPosition;
            this.Velocity = 0;
            this.Direction = 0;
            this.Health = 1;
            this.RotationalVelocity = 0;
            this.Energy = 0;
            this.Ore = 0;
            this.Oxygen = 0;
            this.Water = 0;
            this.gunLevel = 0;
        }

        public virtual int GunLevel
        {
            get
            {
                return gunLevel;
            }
            set
            {
                this.gunLevel = value;
            }
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2((float)(X + Width / 2), (float)(Y + Height / 2));
            }
        }

        public Vector2 ShipFront
        {
            get
            {
                return new Vector2((float)(Center.X + ((Width - 10) / 2 * Math.Cos(Direction))), (float)(Center.Y + ((Height - 10) / 2 * Math.Sin(Direction))));
            }
        }

        public override void Update(List<ShardObject> shardObjects, GameTime gameTime)
        {
            base.Update(shardObjects, gameTime);
            foreach (ShardObject shardObject in shardObjects)
            {
                if (shardObject is Resource)
                {
                    if (shardObject.IsValid() && this.GetBounds().Intersects(shardObject.GetBounds()))
                    {
                        this.Energy += shardObject.Energy;
                        this.Ore += shardObject.Ore;
                        this.Oxygen += shardObject.Oxygen;
                        this.Water += shardObject.Water;
                        shardObject.SetValid(false);
                    }
                }
            }
        }
    }
}
