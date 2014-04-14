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
    class Button
    {
        private ShardGame gameReference;
        private MenuImage image;

        private Vector2 position;

        public Button() : this(null) { }

        public Button(ShardGame gameReference) : this(gameReference, null) { }

        public Button(ShardGame gameReference, MenuImage image)
        {
            this.gameReference = gameReference;
            this.image = image;
            position = Vector2.Zero;
        }

        #region Fields

        public MenuImage Image
        {
            get { return this.image; }
            set { this.image = value; }
        }

        public bool Visible
        {
            get { return image.Visible; }
            set { this.image.Visible = value; }
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

        public int Width
        {
            get { return image.Width; }
        }

        public int Height
        {
            get { return image.Height; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)this.X, (int)this.Y, this.Width, this.Height); }
        }

        #endregion



    }
}
