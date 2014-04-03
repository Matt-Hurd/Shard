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
        private  int speedLevel;
        private int armorLevel;
        private int gunLevel;
        private int missileLevel;
        private int laserLevel;

        protected int reloadTime;
        protected int rearmTime;

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
            this.missileLevel = 0;
            this.laserLevel = 0;
            this.armorLevel = 0;
            this.reloadTime = 0;
            this.rearmTime = 0;
        }

        #region Modifying and Returning Fields

        public int GunLevel
        {
            get
            {
                return gunLevel;
            }
            set
            {
                if (value > 0)
                    this.gunLevel = value;
                else
                    gunLevel = 0;
            }
        }

        public int MissileLevel
        {
            get
            {
                return missileLevel;
            }
            set
            {
                if (value > 0)
                    this.missileLevel = value;
                else
                    missileLevel = 0;
            }
        }

        public int LaserLevel
        {
            get
            {
                return laserLevel;
            }
            set
            {
                if (value > 0)
                    this.laserLevel = value;
                else
                    laserLevel = 0;
            }
        }

        public int SpeedLevel
        {
            get
            {
                return speedLevel;
            }
            set
            {
                if (value > 0)
                    this.speedLevel = value;
                else
                    speedLevel = 0;
            }
        }

        public int ArmorLevel
        {
            get
            {
                return this.armorLevel;
            }
            set
            {
                if (value > 0)
                    this.armorLevel = value;
                else
                    armorLevel = 0;
            }
        }

        public Vector2 ShipFront
        {
            get
            {
                return new Vector2((float)(Center.X + (Width / 2 * Math.Cos(Direction))), (float)(Center.Y + (Height / 2 * Math.Sin(Direction))));
            }
        }

        #endregion

        #region Shooting and Bullets

        //Should be Overriden for ships with different scaling
        public virtual Projectile GetGunBullet(GameImageSourceDirectory sourceDirectory)
        {
            Projectile p = new Projectile((int)(this.ShipFront.X), (int)(this.ShipFront.Y));
            p.Alignment = this.Alignment;
            p.Direction = this.Direction;
            p.RotationalVelocity = this.RotationalVelocity;
            switch (GunLevel)
            {
                case 0:
                    return null;
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
            p.X -= p.Width/2;
            p.Y -= p.Height/2;
            return p;
        }

        //Should be Overriden for ships with different scaling
        public virtual Missile GetMissile(GameImageSourceDirectory sourceDirectory)
        {
            Missile missile = new Missile((int)(this.ShipFront.X), (int)(this.ShipFront.Y));
            missile.Alignment = this.Alignment;
            missile.Direction = this.Direction;
            missile.RotationalVelocity = 0;
            switch(MissileLevel)
            {
                case 0:
                    return null;
                case 1:
                    missile.ImageSource = sourceDirectory.GetSourceRectangle("shipMissile");
                    missile.TravelSpeed = 4.0;
                    missile.Damage = 2;
                    break;
                default:
                    missile.ImageSource = sourceDirectory.GetSourceRectangle("shipMissile");
                    missile.TravelSpeed = 4.0;
                    missile.Damage = 2;
                    break;

            }
            missile.Width = missile.ImageSource.Width;
            missile.Height = missile.ImageSource.Height;
            missile.X -= missile.Width / 2;
            missile.Y -= missile.Height / 2;
            return missile;
        }

        //Should be Overriden for ships with different scaling
        public virtual int GetReloadTime()
        {
            switch (GunLevel)
            {
                case 0:
                    return 100;
                case 1:
                    return 20;
                default:
                    return 0;
            }
        }

        //Should be Overriden for ships with different scaling
        public virtual int GetRearmTime()
        {
            switch (MissileLevel)
            {
                case 0:
                    return 100;
                case 1:
                    return 30;
                default:
                    return 0;
            }
        }

        public virtual void Shoot(List<ShardObject> shardObjects, GameImageSourceDirectory sourceDirectory)
        {
            if (reloadTime <= 0)
            {
                Projectile bullet = GetGunBullet(sourceDirectory);
                if (bullet != null)
                {
                    shardObjects.Add(bullet);
                    reloadTime = this.GetReloadTime();
                }
            }

            //Missiles
            if (rearmTime <= 0)
            {
                Missile missile = GetMissile(sourceDirectory);
                if (missile != null)
                {
                    missile.SelectTarget(shardObjects);
                    shardObjects.Add(missile);
                    rearmTime = this.GetRearmTime();
                }
            }
        }

        //Changes the given direction +- maximumSway
        protected virtual double SwayDirection(double direction, double maximumSway)
        {
            Random random = new Random();
            double swayAmount = random.NextDouble() * maximumSway * 2;
            return (direction - maximumSway) + swayAmount;
        }

        #endregion

        #region Statistics (Speed, Armor, and Health)

        //Should be Overriden for ships with different scaling
        public virtual double GetMaxSpeed()
        {
            switch (SpeedLevel)
            {
                case 0:
                case 1:
                    return 3.0;
                default:
                    return 3.0;
            }
        }

        #endregion

        public override void Update(List<ShardObject> shardObjects, GameTime gameTime)
        {
            base.Update(shardObjects, gameTime);
            reloadTime -= 1;
            rearmTime -= 1;
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
