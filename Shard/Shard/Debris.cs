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
    class Debris : ShardObject
    {
        public Debris() : this(0, 0) { }

        public Debris(int xPosition, int yPosition)
            : base(xPosition, yPosition)
        {
            Health = 1;
        }

        public override void Update(List<ShardObject> shardObjects, GameTime gameTime)
        {
            checkCollision(shardObjects);
            base.Update(shardObjects, gameTime);
        }

        public void checkCollision(List<ShardObject> x)
        {
            foreach (ShardObject so in x)
            {
                if ((this.GetBounds().Intersects(so.GetBounds())) && (!(this.Equals(so))) && so.Solid)
                {
                    float y2 = this.GetBounds().Center.Y - so.GetBounds().Center.Y;
                    float x2 = this.GetBounds().Center.X - so.GetBounds().Center.X;
                    double ang2 = Math.Atan2(y2, x2);
                    so.HorizontalVelocity = -Math.Cos(ang2) / 2;// *player.Velocity;
                    so.VerticalVelocity = -Math.Sin(ang2) / 2;// *player.Velocity;

                    this.HorizontalVelocity = (Math.Cos(ang2)) / 2;
                    this.VerticalVelocity = (Math.Sin(ang2)) / 2;

                    if (!(so is Debris))
                    {
                        so.Health -= Velocity;
                        this.Health -= Velocity;
                    }

                }
            }
        }
    }
}
