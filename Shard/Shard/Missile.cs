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

        public Missile(int xPosition, int yPosition) : base(xPosition, yPosition) 
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

        public override void Update(List<ShardObject> shardObjects, Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (targetReference != null)
            {
                PointTowards(targetReference.Center);
                Velocity = 0;
            }
            base.Update(shardObjects, gameTime);
        }
    }
}
