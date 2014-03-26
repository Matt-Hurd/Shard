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
        private Ship playerReference;

        public EnemyShip(int xPosition, int yPosition) : base(xPosition, yPosition) 
        {
            ActivationRange = 1000.0;
            playerReference = null;
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

        public virtual Ship Player
        {
            get
            {
                return playerReference;
            }
            set
            {
                this.playerReference = value;
            }
        }

        public bool ProcessPlayer(Ship player)
        {
            Player = player;
            if (EuclideanMath.DistanceBetween(playerReference.Center, this.Center) < ActivationRange) //Only acts against player if within range
            {
                return HandlePlayerInformation(playerReference);
            }
            return false;
        }

        protected virtual bool HandlePlayerInformation(Ship player)
        {
            PointTowards(player.Center);
            return true;
        }

        public override void Update(List<ShardObject> shardObjects, GameTime gameTime)
        {
            base.Update(shardObjects, gameTime);
        }
    }
}
