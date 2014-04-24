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
    //Only used for damage pop-ups

    class ShardGraphic : ShardObject
    {
        string text;
        SpriteFont font;
        Color textColor;

        public ShardGraphic() : this(0, 0) { }

        public ShardGraphic(int xPosition, int yPosition)
            : base(xPosition, yPosition)
        {
            text = null;
            font = null;
            textColor = Color.White;
            Solid = false;
            Health = 30;
        }

        #region Mutating and Returning Fields

        public virtual string Text
        {
            get
            {
                return text;
            }
            set
            {
                this.text = value;
            }
        }

        public virtual SpriteFont Font
        {
            get
            {
                return font;
            }
            set
            {
                this.font = value;
            }
        }

        public virtual Color TextColor
        {
            get
            {
                return textColor;
            }
            set
            {
                this.textColor = value;
            }
        }

        #endregion

        #region Helper Methods

        public bool HasValidText()
        {
            return text != null && !text.Equals("");
        }

        public bool HasValidFont()
        {
            return font != null;
        }

        #endregion

        public override void Update(List<ShardObject> shardObjects, GameTime gameTime)
        {
            Health -= 1;
            base.Update(shardObjects, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            if(HasValidText() && HasValidFont())
            {
                spriteBatch.DrawString(font, text, this.Position, textColor, 0f, new Vector2(0,0), new Vector2(1,1), SpriteEffects.None, (float)Depth);
                //spriteBatch.DrawString(font, text, this.Position, textColor, 0f, new Vector2(0,0), 1.0f, SpriteEffects.None, (float)Depth);
            }
            //base.Draw(spriteBatch, spritesheet);
        }

    }
}
