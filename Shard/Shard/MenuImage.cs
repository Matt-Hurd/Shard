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
    class MenuImage
    {
        private Vector2 position;
        private Rectangle sourceRectangle;
        private float imageDepth;
        private bool isVisible;

        public MenuImage() : this(Vector2.Zero, new Rectangle(0, 0, 1, 1)) { }

        public MenuImage(Vector2 position, Rectangle sourceRectangle)
        {
            this.position = position;
            this.sourceRectangle = sourceRectangle;
            isVisible = true;
            imageDepth = 0;
        }

        #region Fields

        public bool Visible
        {
            get { return this.isVisible; }
            set { this.isVisible = value; }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                this.position = value;
            }
        }

        public double X
        {
            get { return position.X; }
            set { position.X = (float)value; }
        }

        public double Y
        {
            get { return position.Y; }
            set { position.Y = (float)value; }
        }

        public Rectangle ImageSource
        {
            get
            {
                return sourceRectangle;
            }
            set
            {
                this.sourceRectangle = value;
            }
        }

        public int Width
        {
            get { return sourceRectangle.Width; }
        }

        public int Height
        {
            get { return sourceRectangle.Height; }
        }

        public float Depth
        {
            get { return this.imageDepth; }
            set { if (Math.Abs(value) <= 1) { this.imageDepth = value; } }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)this.X, (int)this.Y, this.Width, this.Height); }
        }

        #endregion

        public virtual void Draw(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            if (Visible)
            {
                spriteBatch.Draw(spritesheet, Bounds, ImageSource, Color.White, 0f, Vector2.Zero, SpriteEffects.None, Depth);
            }
        }
    }
}
