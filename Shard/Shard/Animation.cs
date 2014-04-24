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
    class Animation : ShardObject
    {

        public enum Type
        {
            EXPLOSION, OTHER
        }

        private Type type;
        private int incAmt;
        private double rotation;

        public Animation(int xPosition, int yPosition)
            : base(xPosition, yPosition)
        {

            this.Health = 0;
            this.Solid = false;
            incAmt = 0;

            type = Type.EXPLOSION;
            if (type == Type.EXPLOSION)
            {
                Width = 18;
                Height = 16;
            }

        }

        public virtual Double AnimationRot
        {
            get
            {
                return this.rotation;
            }
            set
            {
                this.rotation = value;
            }
        }

        public virtual Type AnimationType
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

        public override void Update(List<ShardObject> shardObjects, Microsoft.Xna.Framework.GameTime gameTime)
        {
            incAmt++;

            if (incAmt >= 30)
            {
                Health++;
                incAmt = 0;
            }

            if (Health == 5)
                SetValid(false);

            base.Update(shardObjects, gameTime);

            //Actual missile collision specifics
        }


        public override void Draw(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            spriteBatch.Draw(spritesheet, GetBounds(), new Rectangle((int)Health * 18, 0, (int)Width, (int)Height), Color.White, -(float)rotation, new Vector2(9,8), SpriteEffects.None, 0);
            
        }


    }
}
