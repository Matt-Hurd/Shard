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
    class MenuText
    {
        private String text;
        private SpriteFont font;
        private Color textColor;
        private Vector2 position;
        private float drawDepth;
        private bool isVisible;

        public MenuText() : this(Vector2.Zero, "", null) { }

        public MenuText(Vector2 position, String text, SpriteFont spriteFont)
        {
            this.position = position;
            this.text = text;
            this.font = spriteFont;
            this.textColor = Color.White;
            isVisible = true;
            drawDepth = 1;
        }

        #region Fields

        public bool Visible
        {
            get { return this.isVisible; }
            set { this.isVisible = value; }
        }

        public String Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        public SpriteFont Font
        {
            get { return this.font; }
            set { this.font = value; }
        }

        public Color Color
        {
            get { return this.textColor; }
            set { this.textColor = value; }
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

        public float Depth
        {
            get { return this.drawDepth; }
            set { if (Math.Abs(value) <= 1) { this.drawDepth = value; } }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)this.X, (int)this.Y, (int)font.MeasureString(text).X, (int)font.MeasureString(text).Y); }
        }

        #endregion

        public virtual void Draw(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            if (Visible && text != null && font != null)
            {
                spriteBatch.DrawString(font, text, this.Position, textColor, 0f, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, Depth);
            }
        }
    }
}