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
        private bool isActive;

        //Underlying Menu Data Structures
        private List<Button> buttons;
        private List<MenuImage> menuImages;
        private GameImageSourceDirectory menuImageSourceDirectory;

        private ShardGame gameReference;

        public GameMenu() : this(null) { }

        public GameMenu(ShardGame gameReference)
        {
            this.gameReference = gameReference;
            this.buttons = new List<Button>();
            this.menuImages = new List<MenuImage>();
            isActive = true;
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

        public bool Active
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
            }
        }

        public void GiveGameReference(ShardGame gameReference)
        {
            this.gameReference = gameReference;
        }

        public void GiveSourceDirectory(GameImageSourceDirectory sourceDirectory)
        {
            this.menuImageSourceDirectory = sourceDirectory;
        }

        #endregion

        public void AddButton(Button button)
        {
            button.GiveMenuReference(this);
            buttons.Add(button);
        }

        public void AddMenuImage(MenuImage menuImage)
        {
            menuImages.Add(menuImage);
        }

        public void HandleMouseState(MouseState previousMouse, MouseState currentMouse)
        {
            foreach (Button button in buttons)
            {
                button.HandleMouseState(previousMouse, currentMouse);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            if (isActive)
            {
                foreach (MenuImage menuImage in menuImages)
                {
                    menuImage.Draw(spriteBatch, spritesheet);
                }
                foreach (Button button in buttons)
                {
                    button.Draw(spriteBatch, spritesheet);
                }
            }
        }
    }
}
