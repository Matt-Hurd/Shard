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
    class BossShip : Follower
    {
        public BossShip(int xPosition, int yPosition)
            : base(xPosition, yPosition)
        {
            GunLevel = 1;
            MissileLevel = 1;
            SpeedLevel = 1;
            ArmorLevel = 1;
            Health = 100;
        }

        public BossShip(XElement node)
            : base(node)
        {
        }

        public override void GetImageSource(GameImageSourceDirectory sourceDirectory)
        {
            this.ImageSource = sourceDirectory.GetSourceRectangle("noImageLarge");
        }

        public override XElement toNode()
        {
            XElement parent = base.toNode();
            var newXml = new XElement("bossship",
                             parent.Attributes(),
                             parent.Elements());
            return newXml;
        }

        #region Scaling Changes

        protected override int GetBulletDamage()
        {
            return 7;
        }

        protected override int GetReloadTime()
        {
            return 40;
        }

        protected override int GetMissileDamage()
        {
            return 10;
        }

        protected override double GetBulletSpeed()
        {
            return 10.0;
        }

        protected override double GetMissileTravelSpeed()
        {
            return 5.6;
        }

        public override int GetDamageReductionFromArmor()
        {
            return 5;
        }

        protected override int GetRearmTime()
        {
            return 70;
        }

        //Should be Overriden for ships with different scaling
        public override double GetMaxSpeed()
        {
            return 2.0;
        }

        #endregion
    }
}
