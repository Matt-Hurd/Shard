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
    class GameMenu
    {
        //Menu Attributes
        private bool pausesGame;
        private bool isVisible;

        //Underlying Menu Data Structures
        private List<Button> buttons;

        private ShardGame gameReference;

        public GameMenu()
        {
            gameReference = null;
        }

        public GameMenu(ShardGame gameReference)
        {
            this.gameReference = gameReference;
        }

        #region Attribute Mutation and Value Retrieval

        public bool PausesGame()
        {
            return pausesGame;
        }

        public void SetGamePauseEffect(bool pausesGame)
        {
            this.pausesGame = pausesGame;
        }

        public bool Visible
        {
            get
            {
                return isVisible;
            }
            set
            {
                isVisible = value;
            }
        }

        public void GiveGameReference(ShardGame gameReference)
        {
            this.gameReference = gameReference;
        }

        #endregion
    }
}
