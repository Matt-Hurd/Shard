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
        private ShardGame gameReference;

        public BossShip(int xPosition, int yPosition, ref SoundPlayer sp)
            : base(xPosition, yPosition, ref sp)
        {
            gameReference = null;
            GunLevel = 1;
            MissileLevel = 1;
            SpeedLevel = 1;
            ArmorLevel = 1;
            Health = 100;
        }

        public BossShip(XElement node, ref SoundPlayer sp)
            : base(node, ref sp)
        {
        }

        public override void GetImageSource(GameImageSourceDirectory sourceDirectory)
        {
            this.ImageSource = sourceDirectory.GetSourceRectangle("pirateShipBoss");
        }

        public override XElement toNode()
        {
            XElement parent = base.toNode();
            var newXml = new XElement("bossship",
                             parent.Attributes(),
                             parent.Elements());
            return newXml;
        }

        public ShardGame GameReference
        {
            get { return gameReference; }
            set { gameReference = value; }
        }

        public override void Update(List<ShardObject> shardObjects, GameTime gameTime)
        {
            base.Update(shardObjects, gameTime);
            if (IsWithinActivationRange())
            {
                if (EuclideanMath.RandomInteger(0, 500) == 13)
                    SpawnSeeker(3);
                if (EuclideanMath.RandomInteger(0, 1000) == 13)
                    SpawnThug(3);
            }
        }

        private void SpawnSeeker(int meanLevel)
        {
            EnemyShip ship = new Seeker(0,0, ref gameReference.soundPlayer);
            ship.MaximumHealth = 30;
            ship.Health = ship.MaximumHealth;
            ship.GunLevel = 5;
            ship.SpeedLevel = 3;
            ship.MissileLevel = 2;
            ship.ArmorLevel = 1;
            ship.Velocity = 0;
            ship.GetImageSource(gameReference.GetGameSourceDirectory());
            ship.Width = ship.ImageSource.Width;
            ship.Height = ship.ImageSource.Height;
            Vector2 openCoords = GetOpenCoordinatesForShipSpawning(ship);
            ship.X = openCoords.X;
            ship.Y = openCoords.Y;
            this.ListReference.Add(ship);
        }

        private void SpawnThug(int meanLevel)
        {
            EnemyShip ship = new Thug(0, 0, ref gameReference.soundPlayer);
            ship.MaximumHealth = 80;
            ship.Health = ship.MaximumHealth;
            ship.GunLevel = 5;
            ship.SpeedLevel = 3;
            ship.MissileLevel = 2;
            ship.ArmorLevel = 5;
            ship.Velocity = 0;
            ship.GetImageSource(gameReference.GetGameSourceDirectory());
            ship.Width = ship.ImageSource.Width;
            ship.Height = ship.ImageSource.Height;
            Vector2 openCoords = GetOpenCoordinatesForShipSpawning(ship);
            ship.X = openCoords.X;
            ship.Y = openCoords.Y;
            this.ListReference.Add(ship);
        }

        private Vector2 GetOpenCoordinatesForShipSpawning(Ship ship)
        {
            Rectangle spawnBounds = new Rectangle((int)this.X - (int)ship.Width - 4, (int)this.Y - (int)ship.Height - 4, (int)this.Width + (int)ship.Width * 2 + 8, (int)this.Height + (int)ship.Height * 2 + 8);
            int potentialX = EuclideanMath.RandomInteger(spawnBounds.X - (int)ship.Width, spawnBounds.X + spawnBounds.Width + (int)ship.Width);
            while(!(potentialX <= spawnBounds.X || potentialX >= spawnBounds.X + spawnBounds.Width))
                potentialX = EuclideanMath.RandomInteger(spawnBounds.X - (int)ship.Width, spawnBounds.X + spawnBounds.Width + (int)ship.Width);
            int potentialY = EuclideanMath.RandomInteger(spawnBounds.Y - (int)ship.Height, spawnBounds.Y + spawnBounds.Height + (int)ship.Height);
            while (!(potentialY <= spawnBounds.Y || potentialY >= spawnBounds.Y + spawnBounds.Height))
                potentialY = EuclideanMath.RandomInteger(spawnBounds.Y - (int)ship.Height, spawnBounds.Y + spawnBounds.Height + (int)ship.Height);
            return new Vector2(potentialX, potentialY);
        }

        public override void Destroy(List<ShardObject> shardObjects, GameImageSourceDirectory sourceDirectory)
        {
            GameReference.ToggleMenu("Win");
            base.Destroy(shardObjects, sourceDirectory);
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
