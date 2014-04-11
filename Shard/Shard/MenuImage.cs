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

        public MenuImage() : this(Vector2.Zero, new Rectangle(0, 0, 1, 1)) { }

        public MenuImage(Vector2 position, Rectangle sourceRectangle)
        {
            this.position = position;
            this.sourceRectangle = sourceRectangle;
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
    }
}
