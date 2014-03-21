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
        public EnemyShip(int xPosition, int yPosition) : base(xPosition, yPosition) { }

        public virtual void ProcessPlayer(Ship player)
        {
            double range = 1000.0;
            if (EuclideanMath.DistanceBetween(player.Center, this.Center) < range) //Only acts against player if within range
            {
                PointTowards(player.Center);
            }
        }

        public override void Update(List<ShardObject> shardObjects, GameTime gameTime)
        {
            Velocity = .5;
            base.Update(shardObjects, gameTime);
        }
    }
}
