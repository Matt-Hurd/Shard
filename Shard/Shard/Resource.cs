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
    //public enum ResourceType
    //{
    //    ORE, OXYGEN, WATER, ENERGY
    //}

    class Resource : ShardObject
    {
        //private ResourceType resourceType;

        public Resource() : this(0,0) { }

        public Resource(int xPosition, int yPosition) : this(xPosition, yPosition, 0, 0, 0, 0) { }

        public Resource(int xPosition, int yPosition, int amountEnergy, int amountOre, int amountOxygen, int amountWater)
        {
            this.X = xPosition;
            this.Y = yPosition;
            this.Energy = amountEnergy;
            this.Ore = amountOre;
            this.Oxygen = amountOxygen;
            this.Water = amountWater;
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            double temp = Direction;
            Direction = 0;
            base.Draw(spriteBatch, spritesheet);
            Direction = temp;
        }

        public override void Update(List<ShardObject> shardObjects, GameTime gameTime)
        {
            base.Update(shardObjects, gameTime);
            foreach (ShardObject shardObject in shardObjects)
            {
                if (shardObject is Ship)
                {
                    if (shardObject.IsValid() && this.GetBounds().Intersects(shardObject.GetBounds()))
                    {
                        if (this.Energy > 0)
                        {
                            shardObject.Energy += this.Energy;
                            ShardGraphic temp = this.CreateDamageGraphic(this.Energy, Color.Plum);
                            temp.Velocity = 0;
                        }
                        if (this.Ore > 0)
                        {
                            shardObject.Ore += this.Ore;
                            ShardGraphic temp = this.CreateDamageGraphic(this.Ore, Color.Ivory);
                            temp.Velocity = 0;
                        }
                        if (this.Oxygen > 0)
                        {
                            shardObject.Oxygen += this.Oxygen;
                            ShardGraphic temp = this.CreateDamageGraphic(this.Oxygen, Color.LightGreen);
                            temp.Velocity = 0;
                        }
                        if (this.Water > 0)
                        {
                            shardObject.Water += this.Water;
                            ShardGraphic temp = this.CreateDamageGraphic(this.Water, Color.LightBlue);
                            temp.Velocity = 0;
                        }
                        this.SetValid(false);
                    }
                }
            }
        }

        public override void Destroy(List<ShardObject> shardObjects, GameImageSourceDirectory sourceDirectory)
        {
            //base.Destroy(shardObjects, sourceDirectory); Don't do this!
            SetValid(false);
        }
    }
}
