using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shard
{
    /*
     *  A big and slow EnemyShip with big fire power, meant to alone
     */
    class Bruiser : Follower
    {
        public Bruiser(int xPosition, int yPosition)
            : base(xPosition, yPosition)
        {
            GunLevel = 1;
            MissileLevel = 1;
            SpeedLevel = 1;
            Health = 100;
        }

        public override void GetImageSource(GameImageSourceDirectory sourceDirectory)
        {
            this.ImageSource = sourceDirectory.GetSourceRectangle("pirateShip2_colored");
        }

        #region Scaling Changes

        protected override int GetBulletDamage()
        {
            switch (GunLevel)
            {
                case 1:
                    return 2;
                case 2:
                    return 3;
                case 3:
                case 4:
                    return 4;
                case 5:
                    return 5;
                default:
                    return 0;

            }
        }

        protected override int GetReloadTime()
        {
            switch (GunLevel)
            {
                case 1:
                    return 60;
                case 2:
                    return 50;
                case 3:
                    return 40;
                case 4:
                    return 35;
                case 5:
                    return 30;
                default:
                    return 100;
            }
        }

        protected override int GetMissileDamage()
        {
            switch (GunLevel)
            {
                case 1:
                    return 3;
                case 2:
                    return 3;
                case 3:
                case 4:
                    return 5;
                case 5:
                    return 7;
                default:
                    return 0;

            }
        }

        protected override double GetMissileTravelSpeed()
        {
            switch (MissileLevel)
            {
                case 1:
                case 2:
                case 3:
                    return 4.5;
                case 4:
                    return 5.0;
                case 5:
                    return 6.0;
                default:
                    return 1.0;
            }
        }


        protected override int GetRearmTime()
        {
            switch (GunLevel)
            {
                case 1:
                    return 100;
                case 2:
                    return 80;
                case 3:
                    return 70;
                case 4:
                case 5:
                    return 60;
                default:
                    return 120;
            }
        }

        //Should be Overriden for ships with different scaling
        public override double GetMaxSpeed()
        {
            switch (SpeedLevel)
            {
                case 1:
                    return 1.5;
                case 2:
                    return 2.0;
                case 3:
                    return 2.5;
                default:
                    return 1.0;
            }
        }

        #endregion
    }
}