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
    class Missile : Projectile
    {
        private ShardObject targetReference;
        private double travelSpeed;
        private double targetSelectDistance;

        public Missile(int xPosition, int yPosition)
            : base(xPosition, yPosition)
        {
            targetReference = null;
            travelSpeed = 1;
            Damage = 1;
            targetSelectDistance = 800.0;
        }

        public virtual ShardObject Target
        {
            get
            {
                return targetReference;
            }
            set
            {
                targetReference = value;
            }
        }

        public virtual double TravelSpeed
        {
            get
            {
                return travelSpeed;
            }
            set
            {
                if (value > 0)
                    travelSpeed = value;
                else
                    travelSpeed = 0;
            }
        }

        public double TargetSelectionDistance
        {
            get
            {
                return targetSelectDistance;
            }
            set
            {
                targetSelectDistance = value;
            }
        }

        public virtual bool SelectTarget(List<ShardObject> shardObjects)
        {
            double lowestDistance = Int32.MaxValue;
            int lowestIndex = -1;
            for (int i = 0; i < shardObjects.Count; i++)
            {
                if (shardObjects[i] is Ship && shardObjects[i].Alignment != this.Alignment)
                {
                    double currentDistance = EuclideanMath.DistanceBetween(this.Center, shardObjects[i].Center);
                    if (currentDistance < lowestDistance && currentDistance > 0 && currentDistance < TargetSelectionDistance)
                    {
                        lowestDistance = currentDistance;
                        lowestIndex = i;
                    }
                }
            }

            if (lowestIndex >= 0)
            {
                targetReference = shardObjects[lowestIndex];
                return true;
            }
            return false;
        }

        public override void Update(List<ShardObject> shardObjects, Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (targetReference != null)
            {
                if (targetReference.IsValid() && CurrentLife > 25)
                {
                    PointTowards(targetReference.Center);
                    Velocity = travelSpeed;
                }
            }
            else
            {
                Velocity = travelSpeed;
            }
            base.Update(shardObjects, gameTime);

            //Actual missile collision specifics
        }
    }
}
