using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace Shard
{
    /*
     *  A medium speed EnemyShip with moderate fire power, meant to fight in groups (Thugs and Seekers)
     */
    class Thug : Follower
    {
        public Thug(int xPosition, int yPosition) : base(xPosition, yPosition) 
        {
            GunLevel = 1;
            MissileLevel = 1;
            SpeedLevel = 1;
            Health = 50;
        }

        public Thug(XElement node)
            : base(node)
        {
        }

        public override XElement toNode()
        {
            XElement parent = base.toNode();
            var newXml = new XElement("thug",
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
                    return 1;
                case 2:
                    return 2;
                case 3:
                    return 3;
                case 4:
                    return 4;
                case 5:
                    return 6;
                default:
                    return 0;

            }
        }

        protected override int GetReloadTime()
        {
            switch (GunLevel)
            {
                case 1:
                    return 70;
                case 2:
                    return 65;
                case 3:
                    return 55;
                case 4:
                    return 45;
                case 5:
                    return 35;
                default:
                    return 100;
            }
        }

        protected override int GetMissileDamage()
        {
            switch (GunLevel)
            {
                case 1:
                    return 1;
                case 2:
                    return 2;
                case 3:
                    return 3;
                case 4:
                    return 4;
                case 5:
                    return 6;
                default:
                    return 0;

            }
        }
        
        protected override double GetMissileTravelSpeed()
        {
            switch (MissileLevel)
            {
                case 1:
                    return 3.0;
                case 2:
                    return 3.35;
                case 3:
                    return 3.75;
                case 4:
                    return 4.25;
                case 5:
                    return 4.75;
                default:
                    return 1.0;
            }
        }


        protected override int GetRearmTime()
        {
            switch (GunLevel)
            {
                case 1:
                    return 120;
                case 2:
                    return 100;
                case 3:
                    return 90;
                case 4:
                    return 80;
                case 5:
                    return 70;
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
                case 4:
                    return 3.0;
                case 5:
                    return 3.5;
                default:
                    return 1.0;
            }
        }

        #endregion
    }
}