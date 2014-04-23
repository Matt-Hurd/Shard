using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace Shard
{
    /*
     *  A fast and agile EnemyShip with weak fire power, meant to fight in groups
     */
    class Seeker : Follower
    {
        public Seeker(int xPosition, int yPosition) : base(xPosition, yPosition) 
        {
            GunLevel = 1;
            MissileLevel = 0;
            SpeedLevel = 1;
            Health = 30;
        }

        public Seeker(XElement node)
            : base(node)
        {
        }

        public override XElement toNode()
        {
            XElement parent = base.toNode();
            var newXml = new XElement("seeker",
                             parent.Attributes(),
                             parent.Elements());
            return newXml;
        }

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
                case 5:
                    return 3;
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
                    return 2.0;
                case 2:
                    return 2.5;
                case 3:
                    return 3.15;
                case 4:
                    return 3.75;
                case 5:
                    return 4.5;
                default:
                    return 1.0;
            }
        }

        #endregion
    }
}
