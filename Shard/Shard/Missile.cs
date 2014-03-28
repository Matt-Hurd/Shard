using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shard
{
    class Missile : Projectile
    {
        private ShardObject targetReference;
        private double travelSpeed;

        public Missile(int xPosition, int yPosition)
            : base(xPosition, yPosition)
        {
            targetReference = null;
            travelSpeed = 1;
            Damage = 1;
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

        public virtual bool SelectTarget(List<ShardObject> shardObjects)
        {
            double lowestDistance = Int32.MaxValue;
            int lowestIndex = -1;
            for (int i = 0; i < shardObjects.Count; i++)
            {
                if (shardObjects[i] is Ship && shardObjects[i].Alignment != this.Alignment)
                {
                    double currentDistance = EuclideanMath.DistanceBetween(this.Center, shardObjects[i].Center);
                    if (currentDistance < lowestDistance && currentDistance > 0)
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
                if (targetReference.IsValid())
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
