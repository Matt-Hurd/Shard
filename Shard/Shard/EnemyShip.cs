using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Shard
{
    class EnemyShip : Ship
    {
        private double activationRange;
        private Ship playerReference;

        public EnemyShip(int xPosition, int yPosition) : base(xPosition, yPosition, false) 
        {
            ActivationRange = 700.0;
            Alignment = Shard.Alignment.EVIL;
            playerReference = null;
        }

        public EnemyShip(XElement node)
            : base(0, 0, false)
        {
            X = Convert.ToDouble(node.Element("x").Value);
            Y = Convert.ToDouble(node.Element("y").Value);
            HorizontalVelocity = Convert.ToDouble(node.Element("horizontalVelocity").Value);
            VerticalVelocity = Convert.ToDouble(node.Element("verticalVelocity").Value);
            Direction = Convert.ToDouble(node.Element("direction").Value);
            Health = Convert.ToDouble(node.Element("Health").Value);
            RotationalVelocity = Convert.ToDouble(node.Element("RotationalVelocity").Value);
            Energy = Convert.ToInt32(node.Element("Energy").Value);
            Ore = Convert.ToInt32(node.Element("Ore").Value);
            Oxygen = Convert.ToInt32(node.Element("Oxygen").Value);
            Water = Convert.ToInt32(node.Element("Water").Value);
            GunLevel = Convert.ToInt32(node.Element("gunLevel").Value);
            MissileLevel = Convert.ToInt32(node.Element("missileLevel").Value);
            LaserLevel = Convert.ToInt32(node.Element("laserLevel").Value);
            ArmorLevel = Convert.ToInt32(node.Element("armorLevel").Value);
            reloadTime = Convert.ToInt32(node.Element("reloadTime").Value);
            rearmTime = Convert.ToInt32(node.Element("rearmTime").Value);
            ActivationRange = Convert.ToDouble(node.Element("ActivationRange").Value);
            Alignment = Shard.Alignment.EVIL;
            playerReference = null;
        }

        #region Scaling Changes

        protected override float GetBulletSway()
        {
            switch (GunLevel)
            {
                case  1:
                    return 10.0f;
                default:
                    return 20.0f;
            }
        }

        protected override double GetBulletSpeed()
        {
            switch (GunLevel)
            {
                case 1:
                    return 5.5;
                case 2:
                    return 6.0;
                case 3:
                    return 6.5;
                case 4:
                    return 7.0;
                case 5:
                    return 7.5;
                default:
                    return 1;
            }
        }

        public virtual void GetImageSource(GameImageSourceDirectory sourceDirectory)
        {
            this.ImageSource = sourceDirectory.GetSourceRectangle("pirateShip1_colored");
        }

        #endregion

        public virtual double ActivationRange
        {
            get
            {
                return activationRange;
            }
            set
            {
                if (value > 0)
                    activationRange = value;
                else
                    activationRange = 0;
            }
        }

        public bool IsWithinActivationRange()
        {
            if (HasPlayerReference())
            {
                if (EuclideanMath.DistanceBetween(playerReference.Center, this.Center) < ActivationRange) //Only acts against player if within range
                {
                    return true;
                }
            }
            return false;
        }


        public bool HasPlayerReference()
        {
            return playerReference != null;
        }

        public Ship GetPlayerReference()
        {
            return playerReference;
        }

        protected Ship Player
        {
            get
            {
                return GetPlayerReference();
            }
        }

        public void SetPlayerReference(Ship player)
        {
            this.playerReference = player;
        }

        public bool ProcessPlayer()
        {
            if (HasPlayerReference())
            {
                if (EuclideanMath.DistanceBetween(playerReference.Center, this.Center) < ActivationRange) //Only acts against player if within range
                {
                    return HandlePlayerInformation();
                }
            }
            return false;
        }

        protected virtual bool HandlePlayerInformation()
        {
            if (HasPlayerReference())
            {
                PointTowards(Player.Center);
                return true;
            }
            return false;
        }

        public override void ShootAll(List<ShardObject> shardObjects, GameImageSourceDirectory sourceDirectory)
        {
            if (IsWithinActivationRange())
            {
                base.ShootAll(shardObjects, sourceDirectory);
            }
        }

        public override void Update(List<ShardObject> shardObjects, GameTime gameTime)
        {
            ProcessPlayer();
            checkCollision(shardObjects);
            if (!IsWithinActivationRange())
                Velocity = 0;
            base.Update(shardObjects, gameTime);
        }

        public void checkCollision(List<ShardObject> x)
        {
            foreach (ShardObject so in x)
            {
                if (so.Solid && (this.GetBounds().Intersects(so.GetBounds())) && (!(this.Equals(so))))
                {
                    if (so.Alignment != this.Alignment || so is Ship)
                    {
                        float y2 = this.GetBounds().Center.Y - so.GetBounds().Center.Y;
                        float x2 = this.GetBounds().Center.X - so.GetBounds().Center.X;
                        double ang2 = Math.Atan2(y2, x2);
                        so.HorizontalVelocity = -Math.Cos(ang2) / 2;// *player.Velocity;
                        so.VerticalVelocity = -Math.Sin(ang2) / 2;// *player.Velocity;

                        this.HorizontalVelocity = (Math.Cos(ang2)) / 2;
                        this.VerticalVelocity = (Math.Sin(ang2)) / 2;

                        //if (!(so is Debris))
                        //{
                        //    so.Health -= Velocity;
                        //    this.Health -= Velocity;
                        //}
                    }
                }
            }
        }
        public override XElement toNode()
        {
            XElement node =
                new XElement("enemyShip",
                    new XElement("x", this.X),
                    new XElement("y", this.Y),
                    new XElement("horizontalVelocity", this.HorizontalVelocity),
                    new XElement("verticalVelocity", this.VerticalVelocity),
                    new XElement("direction", this.Direction),
                    new XElement("speedLevel", SpeedLevel),
                    new XElement("armorLevel", ArmorLevel),
                    new XElement("gunLevel", GunLevel),
                    new XElement("missileLevel", MissileLevel),
                    new XElement("laserLevel", LaserLevel),
                    new XElement("Health", this.Health),
                    new XElement("RotationalVelocity", this.RotationalVelocity),
                    new XElement("Energy", this.Energy),
                    new XElement("Ore", this.Ore),
                    new XElement("Water", this.Water),
                    new XElement("Oxygen", this.Oxygen),
                    new XElement("reloadTime", this.reloadTime),
                    new XElement("rearmTime", this.rearmTime),
                    new XElement("ActivationRange", ActivationRange)
                    );
            return node;
        }
    }
}
