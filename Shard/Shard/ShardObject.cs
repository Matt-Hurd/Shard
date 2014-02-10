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
        private int energyAmount;
        private int oreAmount;
        private int oxygenAmount;
        private int waterAmount;

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

        //Potential to be overriden not necessary for many objects
        public void destroy(List<GameObject> gameObjects)
        {
            //Add resource drops to gameObjects based on resource amounts
            //Remove self from List of gameObjects
        }
    }
}
