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
    class Follower : EnemyShip
    {
        public Follower(int xPosition, int yPosition) : base(xPosition, yPosition) { }

        public override void ProcessPlayer(Ship player)
        {
            if (EuclideanMath.DistanceBetween(player.Center, this.Center) < ActivationRange) //Only acts against player if within range
            {
                PointTowards(player.Center);
            }
        }

        public override double GetMaxSpeed()
        {
            switch (SpeedLevel)
            {
                default:
                    return 0.75;
            }
        }

        public override void Update(List<ShardObject> shardObjects, GameTime gameTime)
        {
            Velocity = this.GetMaxSpeed();
            base.Update(shardObjects, gameTime);
        }
    }
}
