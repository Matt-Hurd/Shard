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
    }
}
