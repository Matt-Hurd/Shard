using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Shard
{
    /*
     *  A big and slow EnemyShip with big fire power, meant to alone
     */
    class Bruiser : Follower
    {
        public Bruiser(int xPosition, int yPosition, ref SoundPlayer sp)
            : base(xPosition, yPosition, ref sp)
        {
            GunLevel = 1;
            MissileLevel = 1;
            SpeedLevel = 1;
            ArmorLevel = 1;
            Health = 100;
        }

        public Bruiser(XElement node, ref SoundPlayer sp)
            : base(node, ref sp)
        {
        }

        public override void GetImageSource(GameImageSourceDirectory sourceDirectory)
        {
            this.ImageSource = sourceDirectory.GetSourceRectangle("pirateShip2_colored");
        }

        public override XElement toNode()
        {
            XElement parent = base.toNode();
            var newXml = new XElement("bruiser",
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
                    return 2;
                case 2:
                    return 3;
                case 3:
                    return 4;
                case 4:
                    return 6;
                case 5:
                    return 7;
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
            switch (MissileLevel)
            {
                case 1:
                    return 3;
                case 2:
                    return 4;
                case 3:
                    return 5;
                case 4:
                    return 7;
                case 5:
                    return 9;
                default:
                    return 0;

            }
        }

        protected override double GetMissileTravelSpeed()
        {
            switch (MissileLevel)
            {
                case 1:
                    return 2.5;
                case 2:
                    return 3.0;
                case 3:
                    return 3.5;
                case 4:
                    return 4.0;
                case 5:
                    return 4.75;
                default:
                    return 1.0;
            }
        }


        protected override int GetRearmTime()
        {
            switch (MissileLevel)
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
                    return 1.25;
                case 2:
                    return 1.75;
                case 3:
                    return 2.25;
                case 4:
                    return 2.75;
                case 5:
                    return 3.25;
                default:
                    return 1.0;
            }
        }

        #endregion
    }
}