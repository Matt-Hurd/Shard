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
    class EnemyShip : Ship
    {
        private double activationRange;

        public EnemyShip(int xPosition, int yPosition) : base(xPosition, yPosition) 
        {
            ActivationRange = 1000.0;
        }

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

        public virtual void ProcessPlayer(Ship player)
        {
            if (EuclideanMath.DistanceBetween(player.Center, this.Center) < ActivationRange) //Only acts against player if within range
            {
                PointTowards(player.Center);
            }
        }

        public override void Update(List<ShardObject> shardObjects, GameTime gameTime)
        {
            Velocity = GetMaxSpeed();
            base.Update(shardObjects, gameTime);
        }
    }
}
