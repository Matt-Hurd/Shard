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
    abstract class ShardObject : GameObject
    {
        private Rectangle sourceRectangle;
        private Vector2 dimensions;

        private int energyAmount;
        private int oreAmount;
        private int oxygenAmount;
        private int waterAmount;

        public ShardObject() : this(0,0)
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
                shardObjects.Add(water);
            }
            
            SetValid(false);
        }
    }
}
