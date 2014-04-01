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
        public void checkCollision(List<ShardObject> x)
        {
            bool flag = true;
            foreach (ShardObject so in x)
            {
                if (flag)
                {
                    //if (((this is Debris) && (so is Debris)) || (((this is Ship) && (so is Debris)) || ((this is Debris) && (so is Ship))))
                    //{
                        if ((this.GetBounds().Intersects(so.GetBounds())) && (!(this.Equals(so))))
                        {
                            float y2 = this.GetBounds().Center.Y - so.GetBounds().Center.Y;
                            float x2 = this.GetBounds().Center.X - so.GetBounds().Center.X;
                            double ang2 = Math.Atan2(y2, x2);
                            so.HorizontalVelocity = -Math.Cos(ang2) / 2;// *player.Velocity;
                            so.VerticalVelocity = -Math.Sin(ang2) / 2;// *player.Velocity;

                            this.HorizontalVelocity = (Math.Cos(ang2)) / 2;
                            this.VerticalVelocity = (Math.Sin(ang2)) / 2;

                            //if (((this is Ship) && (so is Debris)) || ((this is Debris) && (so is Ship)))
                            if(!((so is Resource)||(so is Debris)))
                            {
                                so.Health -= Velocity;
                                this.Health -= Velocity;
                            }

                            flag = false;
                        }
                    //}
                }
                //if (this is Debris)
                //{
                //    if ((this.GetBounds().Left <= 0) || (this.GetBounds().Right >= 800))
                //        this.HorizontalVelocity *= -1;
                //    if ((this.GetBounds().Top <= 0) || (this.GetBounds().Bottom >= 480))
                //        this.VerticalVelocity *= -1;
                //}
            }
        }

    }
}
