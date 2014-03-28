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
    public enum Alignment
    {
        GOOD, EVIL, NEUTRAL
    }

    abstract class ShardObject : GameObject
    {
        private Rectangle sourceRectangle;
        private Vector2 dimensions;

        private int energyAmount;
        private int oreAmount;
        private int oxygenAmount;
        private int waterAmount;

        private bool isSolid;
        private Alignment alignment;

        public ShardObject()
            : this(0, 0)
        {

        }

        public ShardObject(int xPosition, int yPosition)
        {
            this.sourceRectangle = new Rectangle(0, 0, 1, 1);
            this.Width = 1;
            this.Height = 1;
            this.X = xPosition;
            this.Y = yPosition;
            this.Velocity = 0;
            this.Direction = 0;
            this.Health = 1;
            this.RotationalVelocity = 0;
            this.Energy = 0;
            this.Ore = 0;
            this.Oxygen = 0;
            this.Water = 0;
            this.isSolid = true;
            this.alignment = Alignment.NEUTRAL;
            SetValid(true);
        }

        #region Resource Amount Mutation

        public virtual int Energy
        {
            get
            {
                return energyAmount;
            }
            set
            {
                if (value >= 0)
                    energyAmount = value;
            }
        }

        public virtual int Ore
        {
            get
            {
                return oreAmount;
            }
            set
            {
                if (value >= 0)
                    oreAmount = value;
            }
        }

        public virtual int Oxygen
        {
            get
            {
                return oxygenAmount;
            }
            set
            {
                if (value >= 0)
                    oxygenAmount = value;
            }
        }

        public virtual int Water
        {
            get
            {
                return waterAmount;
            }
            set
            {
                if (value >= 0)
                    waterAmount = value;
            }
        }

        #endregion

        #region Dimensions and Source

        public virtual Rectangle ImageSource
        {
            get
            {
                return sourceRectangle;
            }
            set
            {
                sourceRectangle = value;
            }
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2((float)(X + Width / 2), (float)(Y + Height / 2));
            }
        }

        public virtual float Width
        {
            get
            {
                return dimensions.X;
            }
            set
            {
                dimensions.X = (float)value;
            }
        }

        public virtual float Height
        {
            get
            {
                return dimensions.Y;
            }
            set
            {
                dimensions.Y = (float)value;
            }
        }

        #endregion

        public virtual bool Solid
        {
            get
            {
                return isSolid;
            }
            set
            {
                isSolid = value;
            }
        }

        public virtual Alignment Alignment
        {
            get
            {
                return this.alignment;
            }
            set
            {
                this.alignment = value;
            }
        }


        /*
         * Allows for any specifics in the handling of health changes, this should be called instead of changing Health (unless you want the Health to be exactly a desired value)
         */
        public virtual void ApplyDamage(int damage)
        {
            Health -= damage;
        }

        public override Rectangle GetBounds()
        {
            return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
        }

        public override void Update(List<GameObject> gameObjects, GameTime gameTime)
        {
            List<ShardObject> validShardObjects = new List<ShardObject>();
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject is ShardObject)
                    validShardObjects.Add((ShardObject)gameObject);
            }
            Update(validShardObjects, gameTime);
        }

        public virtual void Update(List<ShardObject> shardObjects, GameTime gameTime)
        {
            Move();
            Direction += RotationalVelocity;
            if (Health <= 0)
                SetValid(false);
            checkCollision(shardObjects);
        }

        public void PointTowards(Vector2 point)
        {
            this.Direction = Math.Atan2(point.Y - this.Center.Y, point.X - this.Center.X);
        }

        public void checkCollision(List<ShardObject> x)
        {
            bool flag = true;
            foreach (ShardObject so in x)
            {
                if (flag)
                {
                    if (((this is Debris) && (so is Debris)) || (((this is Ship) && (so is Debris)) || ((this is Debris) && (so is Ship))))
                    {
                        if ((this.GetBounds().Intersects(so.GetBounds())) && (!(this.Equals(so))))
                        {
                            float y2 = this.GetBounds().Center.Y - so.GetBounds().Center.Y;
                            float x2 = this.GetBounds().Center.X - so.GetBounds().Center.X;
                            double ang2 = Math.Atan2(y2, x2);
                            so.HorizontalVelocity = -Math.Cos(ang2)/2;// *player.Velocity;
                            so.VerticalVelocity = -Math.Sin(ang2)/2;// *player.Velocity;

                            this.HorizontalVelocity = (Math.Cos(ang2)) / 2;
                            this.VerticalVelocity = (Math.Sin(ang2)) / 2;

                            if (((this is Ship) && (so is Debris)) || ((this is Debris) && (so is Ship)))
                            {
                                so.Health -= Velocity;
                                this.Health -= Velocity;
                            }

                            flag = false;
                        }
                    }
                }
                if (this is Debris)
                {
                    if ((this.GetBounds().Left <= 0) || (this.GetBounds().Right >= 800))
                        this.HorizontalVelocity *= -1;
                    if ((this.GetBounds().Top <= 0) || (this.GetBounds().Bottom >= 480))
                        this.VerticalVelocity *= -1;
                }
            }
        }

        public override bool Intersects(GameObject gameObject)
        {
            return GetBounds().Intersects(gameObject.GetBounds());
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            Rectangle correctedPosition = new Rectangle((int)X + (int)Width / 2, (int)Y + (int)Height / 2, (int)Width, (int)Height);
            spriteBatch.Draw(spritesheet, correctedPosition, ImageSource, Color.White, (float)Direction, new Vector2(Width / 2, Height / 2), SpriteEffects.None, 0f);
        }

        //public override void Draw(SpriteBatch spriteBatch, Texture2D spritesheet)
        //{
        //    spriteBatch.Draw(spritesheet, GetBounds(), ImageSource, Color.White);
        //}

        //Potential to be overriden not necessary for many objects
        public virtual void Destroy(List<ShardObject> shardObjects, GameImageSourceDirectory sourceDirectory)
        {
            //Add resource drops to shardObjects based on resource amounts
            Random random = new Random();
            if (energyAmount > 0)
            {
                Resource energy = new Resource((int)X + (int)(random.NextDouble() * Width), (int)Y + (int)(random.NextDouble() * Height), energyAmount, 0, 0, 0);
                energy.Direction = random.NextDouble() * Math.PI * 2;
                energy.Velocity = random.NextDouble() * .5;
                energy.ImageSource = sourceDirectory.GetSourceRectangle("icon_fuel");
                energy.Width = energy.ImageSource.Width;
                energy.Height = energy.ImageSource.Height;
                energy.Solid = false;
                shardObjects.Add(energy);
            }
            if (oreAmount > 0)
            {
                Resource ore = new Resource((int)X + (int)(random.NextDouble() * Width), (int)Y + (int)(random.NextDouble() * Height), 0, oreAmount, 0, 0);
                ore.Direction = random.NextDouble() * Math.PI * 2;
                ore.Velocity = random.NextDouble() * .5;
                ore.ImageSource = sourceDirectory.GetSourceRectangle("icon_ore");
                ore.Width = ore.ImageSource.Width;
                ore.Height = ore.ImageSource.Height;
                ore.Solid = false;
                shardObjects.Add(ore);
            }
            if (oxygenAmount > 0)
            {
                Resource oxygen = new Resource((int)X + (int)(random.NextDouble() * Width), (int)Y + (int)(random.NextDouble() * Height), 0, 0, oxygenAmount, 0);
                oxygen.Direction = random.NextDouble() * Math.PI * 2;
                oxygen.Velocity = random.NextDouble() * .5;
                oxygen.ImageSource = sourceDirectory.GetSourceRectangle("icon_oxygen");
                oxygen.Width = oxygen.ImageSource.Width;
                oxygen.Height = oxygen.ImageSource.Height;
                oxygen.Solid = false;
                shardObjects.Add(oxygen);
            }
            if (waterAmount > 0)
            {
                Resource water = new Resource((int)X + (int)(random.NextDouble() * Width), (int)Y + (int)(random.NextDouble() * Height), 0, 0, 0, waterAmount);
                water.Direction = random.NextDouble() * Math.PI * 2;
                water.Velocity = random.NextDouble() * .5;
                water.ImageSource = sourceDirectory.GetSourceRectangle("icon_water");
                water.Width = water.ImageSource.Width;
                water.Height = water.ImageSource.Height;
                water.Solid = false;
                shardObjects.Add(water);
            }

            SetValid(false);
        }
    }
}
