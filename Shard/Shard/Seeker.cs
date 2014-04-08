using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shard
{
    /*
     *  A fast and agile EnemyShip with weak fire power, meant to fight in groups
     */
    class Seeker : Follower
    {
        public Seeker(int xPosition, int yPosition) : base(xPosition, yPosition) { }

        #region Scaling Changes

        protected override int GetBulletDamage()
        {
            switch (GunLevel)
            {
                case 1:
                case 2:
                    return 1;
                case 3:
                case 4:
                    return 2;
                default:
                    return 0;
                    
            }
        }

        protected override int GetReloadTime()
        {
            switch (GunLevel)
            {
                case 1:
                    return 50;
                case 2:
                    return 40;
                case 3:
                    return 30;
                case 4:
                    return 20;
                default:
                    return 100;
            }
        }

        //Should be Overriden for ships with different scaling
        public override double GetMaxSpeed()
        {
            switch (SpeedLevel)
            {
                case 1:
                    return 5.0;
                case 2:
                    return 6.0;
                case 3:
                    return 7.0;
                default:
                    return 1.0;
            }
        }

        #endregion
    }
}
