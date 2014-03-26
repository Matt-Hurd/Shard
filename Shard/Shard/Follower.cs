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
        /*
         * Behavior: Follows the player at a distance and attempts to utilize weapons against the player. 
         */

        private double followingDistance;

        public Follower(int xPosition, int yPosition) : base(xPosition, yPosition) 
        {
            followingDistance = 100.0;
        }

        #region Mutating and Returning Fields

        public virtual double FollowingDistance
        {
            get
            {
                return followingDistance;
            }
            set
            {
                if (value > 0)
                    followingDistance = value;
                else
                    followingDistance = 0;
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

        #endregion

        #region Updating State

        protected override bool HandlePlayerInformation(Ship player)
        {
            return base.HandlePlayerInformation(player);
        }

        public override void Shoot(List<ShardObject> shardObjects, GameImageSourceDirectory sourceDirectory)
        {
            base.Shoot(shardObjects, sourceDirectory);
        }

        public override void Update(List<ShardObject> shardObjects, GameTime gameTime)
        {
            if (EuclideanMath.DistanceBetween(Player.Center, this.Center) > followingDistance)
            {
                double velocityIncrement = GetMaxSpeed() / 40.0;
                if (Velocity < this.GetMaxSpeed())
                {
                    HorizontalVelocity += Math.Cos(this.Direction) * velocityIncrement;
                    VerticalVelocity += Math.Sin(this.Direction) * velocityIncrement;
                }
            }
            else
            {
                Velocity = 0;
            }
            base.Update(shardObjects, gameTime);
        }

        #endregion
    }
}
