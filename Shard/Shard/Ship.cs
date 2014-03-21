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

        #region Modifying and Returning Fields

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

        public Vector2 ShipFront
        {
            get
            {
                return new Vector2((float)(Center.X + ((Width - 10) / 2 * Math.Cos(Direction))), (float)(Center.Y + ((Height - 10) / 2 * Math.Sin(Direction))));
            }
        }

        #endregion

        public virtual Projectile GetGunBullet(GameImageSourceDirectory sourceDirectory)
        {
            Projectile p = new Projectile((int)(this.ShipFront.X), (int)(this.ShipFront.Y));
            p.Direction = this.Direction;
            p.RotationalVelocity = this.RotationalVelocity;
            switch (GunLevel)
            {
                case 0:
                case 1:
                    p.ImageSource = sourceDirectory.GetSourceRectangle("shipBullet");
                    p.Direction = SwayDirection(p.Direction, MathHelper.ToRadians(2.0f));
                    p.Velocity = 8;
                    p.Damage = 1;
                    break;
                default:
                    p.ImageSource = sourceDirectory.GetSourceRectangle("shipBullet");
                    p.Direction = SwayDirection(p.Direction, MathHelper.ToRadians(2.0f));
                    p.Velocity = 8;
                    p.Damage = 1;
                    break;
            }
            p.Width = p.ImageSource.Width;
            p.Height = p.ImageSource.Height;
            return p;
        }

        protected virtual double SwayDirection(double direction, double maximumSway)
        {
            Random random = new Random();
            double swayAmount = random.NextDouble() * maximumSway * 2;
            return (direction - maximumSway) + swayAmount;
        }

        public virtual void Shoot(List<ShardObject> shardObjects, GameImageSourceDirectory sourceDirectory)
        {
            shardObjects.Add(GetGunBullet(sourceDirectory));
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
