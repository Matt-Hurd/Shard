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
            Alignment = Shard.Alignment.EVIL;
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

        public bool HasPlayerReference()
        {
            return playerReference != null;
        }

        public Ship GetPlayerReference()
        {
            return playerReference;
        }

        protected Ship Player
        {
            get
            {
                return GetPlayerReference();
            }
        }

        public void SetPlayerReference(Ship player)
        {
            this.playerReference = player;
        }

        public bool ProcessPlayer()
        {
            if (HasPlayerReference())
            {
                if (EuclideanMath.DistanceBetween(playerReference.Center, this.Center) < ActivationRange) //Only acts against player if within range
                {
                    return HandlePlayerInformation();
                }
            }
            return false;
        }

        protected virtual bool HandlePlayerInformation()
        {
            if (HasPlayerReference())
            {
                PointTowards(playerReference.Center);
                return true;
            }
            return false;
        }

        public override void Update(List<ShardObject> shardObjects, GameTime gameTime)
        {
            ProcessPlayer();
            base.Update(shardObjects, gameTime);
        }
    }
}
