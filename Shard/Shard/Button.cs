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

        public virtual void UpdateMenu()
        {
            //Do Nothing
        }

        protected MenuText GetMenuText(String initialWords)
        {
            foreach (MenuText menuText in MenuReference.Texts)
            {
                if (menuText.Text.Contains(":") && menuText.Text.Substring(0, menuText.Text.IndexOf(":")).Equals(initialWords))
                {
                    return menuText;
                }
            }
            return null;
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

    #region Miscellaneous Button Classes

    class CloseButton : Button
    {
        public CloseButton(ShardGame gameReference) : this(gameReference, null) { }

        public CloseButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void PreformMouseClickAction()
        {
            GameReference.ToggleMenu(MenuReference.Name);
        }
    }

    #endregion

    #region Pause Menu Classes

    class OptionsButton : Button
    {
        public OptionsButton(ShardGame gameReference) : this(gameReference, null) { }

        public OptionsButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void PreformMouseClickAction()
        {
            MenuReference.Active = false;
            GameReference.ToggleMenu("Options");
        }
    }

    class UpgradesButton : Button
    {
        public UpgradesButton(ShardGame gameReference) : this(gameReference, null) { }

        public UpgradesButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void PreformMouseClickAction()
        {
            MenuReference.Active = false;
            GameReference.ToggleMenu("Upgrades");
        }
    }

    class SaveMenuButton : Button
    {
        public SaveMenuButton(ShardGame gameReference) : this(gameReference, null) { }

        public SaveMenuButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void PreformMouseClickAction()
        {
            MenuReference.Active = false;
            GameReference.ToggleMenu("Save");
        }
    }

    #endregion

    #region Options Menu Button Classes

    class MuteButton : Button
    {
        public MuteButton(ShardGame gameReference) : this(gameReference, null) { }

        public MuteButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void UpdateMenu()
        {
            MenuText menuText = GetMenuText("Mute");
            if (GameReference.Muted && menuText != null)
                menuText.Text = "Mute: " + "On";
            else
                menuText.Text = "Mute: " + "Off";
        }

        public override void PreformMouseClickAction()
        {
            GameReference.Muted = !GameReference.Muted;
            UpdateMenu();
            //base.PreformMouseClickAction();
        }
    }

    class AutomaticDecelerationToggleButton : Button
    {
        public AutomaticDecelerationToggleButton(ShardGame gameReference) : this(gameReference, null) { }

        public AutomaticDecelerationToggleButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void UpdateMenu()
        {
            MenuText menuText = GetMenuText("Auto-Stop");
            if (GameReference.AutomaticDeceleration && menuText != null)
                menuText.Text = "Auto-Stop: " + "On";
            else
                menuText.Text = "Auto-Stop: " + "Off";
        }

        public override void PreformMouseClickAction()
        {
            GameReference.AutomaticDeceleration = !GameReference.AutomaticDeceleration;
            UpdateMenu();
            //base.PreformMouseClickAction();
        }
    }
    class RealisticSpaceMovementToggleButton : Button
    {
        public RealisticSpaceMovementToggleButton(ShardGame gameReference) : this(gameReference, null) { }

        public RealisticSpaceMovementToggleButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void UpdateMenu()
        {
            MenuText menuText = GetMenuText("Realistic Movement");
            if (GameReference.RealisticSpaceMovement && menuText != null)
                menuText.Text = "Realistic Movement: " + "On";
            else
                menuText.Text = "Realistic Movement: " + "Off";
        }

        public override void PreformMouseClickAction()
        {
            GameReference.RealisticSpaceMovement = !GameReference.RealisticSpaceMovement;
            //Locate MenuText referencing information, change to appropriate value
            UpdateMenu();
            //base.PreformMouseClickAction();
        }
    }

    #endregion

    #region Save Menu Button Classes

    class SaveButton : Button
    {
        public SaveButton(ShardGame gameReference) : this(gameReference, null) { }

        public SaveButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void UpdateMenu()
        {
            MenuText menuText = GetMenuText("Save");
            menuText.Text = "Save: \nLast Save:" + GameReference.SaveTime;
        }

        public override void PreformMouseClickAction()
        {
            GameReference.SaveGame();
            UpdateMenu();
            //base.PreformMouseClickAction();
        }
    }

    class LoadButton : Button
    {
        public LoadButton(ShardGame gameReference) : this(gameReference, null) { }

        public LoadButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void PreformMouseClickAction()
        {
            GameReference.RestartGame();
            UpdateMenu();
        }
    }

    class QuitGameButton : Button
    {
        public QuitGameButton(ShardGame gameReference) : this(gameReference, null) { }

        public QuitGameButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void PreformMouseClickAction()
        {
            GameReference.Exit();
        }
    }

    class QuitAppButton : Button
    {
        public QuitAppButton(ShardGame gameReference) : this(gameReference, null) { }

        public QuitAppButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void PreformMouseClickAction()
        {
            TitleScreen Menu = new TitleScreen();
        }
    }

    #endregion

    #region Upgrade Menu Button Classes

    class RepairButton : Button
    {
        public RepairButton(ShardGame gameReference) : this(gameReference, null) { }

        public RepairButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void UpdateMenu()
        {
            MenuText menuText = GetMenuText("Repair Cost");
            if(menuText != null)
                menuText.Text = "Repair Cost: " + (100 - (int)GameReference.Player.Health) + " Ore";
        }

        public override void PreformMouseClickAction()
        {
            Ship player = GameReference.Player;
            if (player.Health >= 100 || !player.IsValid())
                return;
            else if (player.Health + player.Ore >= 100)
            {
                player.Ore -= (100 - (int)player.Health);
                player.Health = 100;
            }
            else
            {
                player.Health += player.Ore;
                player.Ore = 0;
            }
            UpdateMenu();
            //base.PreformMouseClickAction();
        }
    }

    class UpgradeGunButton : Button
    {
        public UpgradeGunButton(ShardGame gameReference) : this(gameReference, null) { }

        public UpgradeGunButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void UpdateMenu()
        {
            MenuText menuText = GetMenuText("Gun Upgrade Cost");
            Ship player = GameReference.Player;
            if (menuText != null)
            {
                if (player.GunLevel < 5)
                    menuText.Text = "Gun Upgrade Cost: " + (player.GunLevel + 1) + "\n" + player.GunLevel * 25 + " FL, " + player.GunLevel * 35 + " OR, " + player.GunLevel * 15 + " OX";
                else
                    menuText.Text = "Gun Upgrade Cost:\n" + "MAXED!";
            }
        }

        public override void PreformMouseClickAction()
        {
            Ship player = GameReference.Player;
            if (player.GunLevel < 5)
            {
                int fuelCost = player.GunLevel * 25;
                int oreCost = player.GunLevel * 35;
                int oxygenCost = player.GunLevel * 15;
                int waterCost = 0;
                if (player.Energy >= fuelCost && player.Ore >= oreCost && player.Oxygen >= oxygenCost && player.Water >= waterCost)
                {
                    player.GunLevel += 1;
                    player.Energy -= fuelCost;
                    player.Ore -= oreCost;
                    player.Oxygen -= oxygenCost;
                    player.Water -= waterCost;
                    UpdateMenu();
                }
            }
        }
    }

    class UpgradeMissileButton : Button
    {
        public UpgradeMissileButton(ShardGame gameReference) : this(gameReference, null) { }

        public UpgradeMissileButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void UpdateMenu()
        {
            MenuText menuText = GetMenuText("Missile Upgrade Cost");
            Ship player = GameReference.Player;
            if (menuText != null)
            {
                if (player.MissileLevel < 5)
                    menuText.Text = "Missile Upgrade Cost: " + (player.MissileLevel + 1) + "\n" + player.MissileLevel * 30 + " FL, " + player.MissileLevel * 15 + " OR, " + player.MissileLevel * 15 + " WT";
                else
                    menuText.Text = "Missile Upgrade Cost:\n" + "MAXED!";
            }
        }

        public override void PreformMouseClickAction()
        {
            Ship player = GameReference.Player;
            if (player.MissileLevel < 5)
            {
                int fuelCost = player.MissileLevel * 30;
                int oreCost = player.MissileLevel * 15;
                int oxygenCost = player.MissileLevel * 0;
                int waterCost = player.MissileLevel * 15;
                if (player.Energy >= fuelCost && player.Ore >= oreCost && player.Oxygen >= oxygenCost && player.Water >= waterCost)
                {
                    player.MissileLevel += 1;
                    player.Energy -= fuelCost;
                    player.Ore -= oreCost;
                    player.Oxygen -= oxygenCost;
                    player.Water -= waterCost;
                    UpdateMenu();
                }
            }
        }
    }

    class UpgradeSpeedButton : Button
    {
        public UpgradeSpeedButton(ShardGame gameReference) : this(gameReference, null) { }

        public UpgradeSpeedButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void UpdateMenu()
        {
            MenuText menuText = GetMenuText("Speed Upgrade Cost");
            Ship player = GameReference.Player;
            if (menuText != null)
            {
                if (player.SpeedLevel < 5)
                    menuText.Text = "Speed Upgrade Cost: " + (player.SpeedLevel + 1) + "\n" + player.SpeedLevel * 20 + " FL, " + player.SpeedLevel * 20 + " OX, " + player.SpeedLevel * 20 + " WT";
                else
                    menuText.Text = "Speed Upgrade Cost:\n" + "MAXED!";
            }
        }

        public override void PreformMouseClickAction()
        {
            Ship player = GameReference.Player;
            if (player.SpeedLevel < 5)
            {
                int fuelCost = player.SpeedLevel * 20;
                int oreCost = player.SpeedLevel * 0;
                int oxygenCost = player.SpeedLevel * 20;
                int waterCost = player.SpeedLevel * 20;
                if (player.Energy >= fuelCost && player.Ore >= oreCost && player.Oxygen >= oxygenCost && player.Water >= waterCost)
                {
                    player.SpeedLevel += 1;
                    player.Energy -= fuelCost;
                    player.Ore -= oreCost;
                    player.Oxygen -= oxygenCost;
                    player.Water -= waterCost;
                    UpdateMenu();
                }
            }
        }
    }

    class UpgradeArmorButton : Button
    {
        public UpgradeArmorButton(ShardGame gameReference) : this(gameReference, null) { }

        public UpgradeArmorButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void UpdateMenu()
        {
            MenuText menuText = GetMenuText("Armor Upgrade Cost");
            Ship player = GameReference.Player;
            if (menuText != null)
            {
                if (player.ArmorLevel < 5)
                    menuText.Text = "Armor Upgrade Cost: " + (player.ArmorLevel + 1) + "\n" + player.ArmorLevel * 15 + " OR, " + player.ArmorLevel * 20 + " OX, " + player.ArmorLevel * 25 + " WT";
                else
                    menuText.Text = "Armor Upgrade Cost:\n" + "MAXED!";
            }
        }

        public override void PreformMouseClickAction()
        {
            Ship player = GameReference.Player;
            if (player.ArmorLevel < 5)
            {
                int fuelCost = player.ArmorLevel * 0;
                int oreCost = player.ArmorLevel * 15;
                int oxygenCost = player.ArmorLevel * 20;
                int waterCost = player.ArmorLevel * 25;
                if (player.Energy >= fuelCost && player.Ore >= oreCost && player.Oxygen >= oxygenCost && player.Water >= waterCost)
                {
                    player.ArmorLevel += 1;
                    player.Energy -= fuelCost;
                    player.Ore -= oreCost;
                    player.Oxygen -= oxygenCost;
                    player.Water -= waterCost;
                    UpdateMenu();
                }
            }
        }
    }

    #endregion

    #region Game Over Menu Button Classes

    class RestartButton : Button
    {
        public RestartButton(ShardGame gameReference) : this(gameReference, null) { }

        public RestartButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void PreformMouseClickAction()
        {
            GameReference.RestartGame();
            MenuReference.Active = false;
        }
    }

    #endregion
}
