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
        public void Destroy(List<GameObject> gameObjects)
        {
            //Add resource drops to gameObjects based on resource amounts
            //Remove self from List of gameObjects
        }
    }
}
