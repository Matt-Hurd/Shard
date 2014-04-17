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
        private GameMenu menuReference;
        private MenuImage image;
        private Color drawColor;

        private Vector2 position;

        public Button() : this(null) { }

        public Button(ShardGame gameReference) : this(gameReference, null) { }

        public Button(ShardGame gameReference, MenuImage image)
        {
            this.gameReference = gameReference;
            position = Vector2.Zero;
            this.Image = image;
            drawColor = Color.Red;
        }

        #region Fields

        public void GiveMenuReference(GameMenu menu)
        {
            this.menuReference = menu;
        }

        public GameMenu MenuReference
        {
            get { return menuReference; }
        }

        public ShardGame GameReference
        {
            get { return gameReference; }
        }

        public MenuImage Image
        {
            get { return this.image; }
            set { this.image = value; this.X = value.X; this.Y = value.Y; }
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
            set { position.X = (float)value; image.X = (float)value; }
        }

        public double Y
        {
            get { return position.Y; }
            set { position.Y = (float)value; image.Y = (float)value; }
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

        public void HandleMouseState(MouseState previousMouse, MouseState currentMouse)
        {
            if (Bounds.Contains(currentMouse.X, currentMouse.Y))
            {
                if (currentMouse.LeftButton.Equals(ButtonState.Pressed))
                    drawColor = new Color(240, 163, 163);
                else
                    drawColor = Color.LightGray;
                if (ReleaseEdgeDetect(previousMouse, currentMouse))
                    PreformMouseClickAction();
            }
            else
                drawColor = Color.White;
        }

        public virtual void PreformMouseClickAction()
        {
            //Do Nothing
            //gameReference.Paused = !gameReference.Paused;
            //menuReference.Active = false;
        }

        private bool ReleaseEdgeDetect(MouseState previous, MouseState current)
        {
            return (current.LeftButton.Equals(ButtonState.Released) && previous.LeftButton.Equals(ButtonState.Pressed));
        }

        public virtual void Draw(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            if (Visible)
            {
                this.Image.Draw(spriteBatch, spritesheet, drawColor);
            }
        }
    }

    #region Upgrade Menu Button Classes

    class RepairButton : Button
    {
        public RepairButton(ShardGame gameReference) : this(gameReference, null) { }

        public RepairButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void PreformMouseClickAction()
        {
            Ship playah = GameReference.Player;
            if(playah.Health >= 100 || !playah.IsValid())
                return;
            else if (playah.Health + playah.Ore >= 100)
            {
                playah.Ore -= (100 - (int)playah.Health);
                playah.Health = 100;
            }
            else
            {
                playah.Health += playah.Ore;
                playah.Ore = 0;
            }
            //base.PreformMouseClickAction();
        }
    }

    class UpgradeGunButton : Button
    {
        public UpgradeGunButton(ShardGame gameReference) : this(gameReference, null) { }

        public UpgradeGunButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void PreformMouseClickAction()
        {
            Ship playah = GameReference.Player;
            if (playah.GunLevel < 5)
            {
                int fuelCost = playah.GunLevel * 25;
                int oreCost = playah.GunLevel * 35;
                int oxygenCost = playah.GunLevel * 15;
                int waterCost = 0;
                if (playah.Energy >= fuelCost && playah.Ore > oreCost && playah.Oxygen > oxygenCost && playah.Water > waterCost)
                {
                    playah.GunLevel += 1;
                }
            }
        }
    }

    #endregion
}
