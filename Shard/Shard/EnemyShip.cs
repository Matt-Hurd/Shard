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

        #region Scaling Changes

        protected override float GetBulletSway()
        {
            switch (GunLevel)
            {
                case  1:
                    return 10.0f;
                default:
                    return 20.0f;
            }
        }

        protected override int GetBulletSpeed()
        {
            switch (GunLevel)
            {
                case 1:
                    return 6;
                case 2:
                    return 7;
                case 3:
                case 4:
                case 5:
                    return 8;
                default:
                    return 1;
            }
        }

        public virtual void GetImageSource(GameImageSourceDirectory sourceDirectory)
        {
            this.ImageSource = sourceDirectory.GetSourceRectangle("pirateShip1_colored");
        }

        #endregion

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
                PointTowards(Player.Center);
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
