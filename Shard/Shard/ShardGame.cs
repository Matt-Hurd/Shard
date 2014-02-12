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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ShardGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D spritesheet;

        KeyboardState previousKeyboard;
        GamePadState previousGamePad;

        List<GameObject> gameObjects;
        Ship player;

        #region Database Fields
        DatabaseConnection objConnect;
        string conString;

        DataSet ds;
        //DataRow dRow;

        int MaxRows;
        //int inc = 0;
        #endregion

        public ShardGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.Window.Title = "Shard";

            #region Database Connection

            try
            {
                objConnect = new DatabaseConnection();
                conString = Shard.Properties.Settings.Default.DatabaseConnectionString;
                objConnect.Connection_string = conString;
                objConnect.Sql = Shard.Properties.Settings.Default.SQL_LoginInformation;
                ds = objConnect.GetConnection;
                MaxRows = ds.Tables[0].Rows.Count;

                //NavigateRecords();
            }
            catch (Exception err)
            {
                Console.Out.Write(err.Message);
            }

#endregion

            gameObjects = new List<GameObject>();

            player = new Ship(100,100);
            player.Width = 32;
            player.Height = 32;
            player.ImageSource = new Rectangle(0,0,32,32);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            spritesheet = Content.Load<Texture2D>("playerShip1_colored");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            GamePadState currentGamePad = GamePad.GetState(PlayerIndex.One);
            KeyboardState currentKeyboard = Keyboard.GetState();

            // Exit Game
            if (currentGamePad.Buttons.Back == ButtonState.Pressed || currentKeyboard.IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here

            //Rotation
            if (currentKeyboard.IsKeyDown(Keys.Left))
                player.Direction -= 0.05;
            if (currentKeyboard.IsKeyDown(Keys.Right))
                player.Direction += 0.05;

            //Movement
            double maxVelocity = 3;

            if (currentKeyboard.IsKeyDown(Keys.Up) && !currentKeyboard.IsKeyDown(Keys.Down))
                player.Velocity += .1;
            else if (currentKeyboard.IsKeyDown(Keys.Down) && !currentKeyboard.IsKeyDown(Keys.Up))
                player.Velocity -= .1;
            else if (!currentKeyboard.IsKeyDown(Keys.Down) && !currentKeyboard.IsKeyDown(Keys.Up))
            {
                if(player.Velocity > 0)
                    player.Velocity -= .05;
                if (player.Velocity < 0)
                    player.Velocity += .05;
                if (player.Velocity < .1 && player.Velocity > -.1)
                    player.Velocity = 0;
            }

            if (player.Velocity > maxVelocity)
                player.Velocity = maxVelocity;
            else if (player.Velocity < -maxVelocity)
                player.Velocity = -maxVelocity;

            player.Update(new List<GameObject>(), gameTime);

            if (player.X > GraphicsDevice.Viewport.Width)
                player.X = 0;
            if (player.X + player.Width < 0)
                player.X = GraphicsDevice.Viewport.Width;
            if (player.Y > GraphicsDevice.Viewport.Height)
                player.Y = 0;
            if (player.Y + player.Height < 0)
                player.Y = GraphicsDevice.Viewport.Height;

            //Update Previous States
            previousGamePad = currentGamePad;
            previousKeyboard = currentKeyboard;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            player.Draw(spriteBatch, spritesheet);
            //spriteBatch.Draw(spritesheet, new Rectangle(32,32,32,32), new Rectangle(0,0,32,32), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
