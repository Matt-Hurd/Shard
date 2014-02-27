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
        GameImageSourceDirectory sourceDirectory;

        KeyboardState previousKeyboard;
        GamePadState previousGamePad;

        //Options
        private bool realisticSpaceMovement;
        private bool automaticDeceleration;

        //Visual Graphics
        protected bool debugVisible;
        protected SpriteFont debugFont;

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
            this.IsMouseVisible = true;

            #region Database Connection

            //try
            //{
            //    objConnect = new DatabaseConnection();
            //    conString = Shard.Properties.Settings.Default.DatabaseConnectionString;
            //    objConnect.Connection_string = conString;
            //    objConnect.Sql = Shard.Properties.Settings.Default.SQL_LoginInformation;
            //    ds = objConnect.GetConnection;
            //    MaxRows = ds.Tables[0].Rows.Count;

            //    //NavigateRecords();
            //}
            //catch (Exception err)
            //{
            //    Console.Out.Write(err.Message);
            //}

            #endregion

            //Default Options
            debugVisible = true;
            realisticSpaceMovement = true;
            automaticDeceleration = true;

            gameObjects = new List<GameObject>();

            player = new Ship(100, 100);
            player.Width = 32;
            player.Height = 32;
            player.ImageSource = new Rectangle(0, 0, 32, 32);

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

            debugFont = Content.Load<SpriteFont>("debugWindowFont");

            //Spritesheet Loading
            spritesheet = Content.Load<Texture2D>("spritesheet_shard_i1");
            sourceDirectory = new GameImageSourceDirectory();
            sourceDirectory.LoadSourcesFromFile("spritesheet_shard_i1.txt");
            player.ImageSource = sourceDirectory.GetSourceRectangle("asteroid_large1_shaded");
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

            //Rotation Control
            double maximumRotationalVelocity = Math.PI / 16.0;
            double rotationalVelocityIncrement = .001;
            double directionalChangeIncrement = .05;

            if (realisticSpaceMovement)
            {
                if (currentKeyboard.IsKeyDown(Keys.Left))
                {
                    if(player.RotationalVelocity > -maximumRotationalVelocity)
                        player.RotationalVelocity -= rotationalVelocityIncrement;
                }
                if (currentKeyboard.IsKeyDown(Keys.Right))
                {
                    if(player.RotationalVelocity < maximumRotationalVelocity)
                        player.RotationalVelocity += rotationalVelocityIncrement;
                }
            }
            else
            {
                if (currentKeyboard.IsKeyDown(Keys.Left))
                    player.Direction -= directionalChangeIncrement;
                if (currentKeyboard.IsKeyDown(Keys.Right))
                    player.Direction += directionalChangeIncrement;

                //player.Velocity = player.Velocity; //Looks weird, is necessary
            }

            //Movement
            double maxVelocity = 3;
            double velocityIncrement = .1;


            //Changes must be made directly to horizontal/vertical velocity of ship to simulate movement within space
            if (realisticSpaceMovement)
            {
                if (currentKeyboard.IsKeyDown(Keys.Up))
                {
                    player.HorizontalVelocity += Math.Cos(player.Direction) * velocityIncrement;
                    player.VerticalVelocity += Math.Sin(player.Direction) * velocityIncrement;
                }
                else if (currentKeyboard.IsKeyDown(Keys.Down))
                {
                    player.HorizontalVelocity += Math.Cos(player.Direction + Math.PI) * velocityIncrement;
                    player.VerticalVelocity += Math.Sin(player.Direction + Math.PI) * velocityIncrement;
                }
            }
            else //Ship Movement without velocity/rotation preservation
            {
                if (currentKeyboard.IsKeyDown(Keys.Up))
                {
                    player.HorizontalVelocity += Math.Cos(player.Direction) * velocityIncrement;
                    player.VerticalVelocity += Math.Sin(player.Direction) * velocityIncrement;
                }
                else if (currentKeyboard.IsKeyDown(Keys.Down)) // <-- Problems
                {
                    player.HorizontalVelocity -= Math.Cos(player.Direction) * velocityIncrement;
                    player.VerticalVelocity -= Math.Sin(player.Direction) * velocityIncrement;
                }
            }

            //Automatic Deceleration of Player Ship Movement and Rotation
            if (automaticDeceleration)
            {
                //Player Rotation Deceleration
                if (!currentKeyboard.IsKeyDown(Keys.Left) && !currentKeyboard.IsKeyDown(Keys.Right))
                {
                    if (Math.Abs(player.RotationalVelocity) < rotationalVelocityIncrement)
                        player.RotationalVelocity = 0;
                    else
                    {
                        if (GetSign(player.RotationalVelocity) > 0)
                            player.RotationalVelocity -= rotationalVelocityIncrement / 2;
                        else if(GetSign(player.RotationalVelocity) < 0)
                            player.RotationalVelocity += rotationalVelocityIncrement / 2;
                    }
                }

                //Player Movement Deceleration
                if (!currentKeyboard.IsKeyDown(Keys.Down) && !currentKeyboard.IsKeyDown(Keys.Up))
                {
                    if (player.Velocity < velocityIncrement)
                        player.Velocity = 0;
                    else
                    {
                        if (realisticSpaceMovement)
                        {
                            //double directionOfVelocity = Math.Atan2(player.VerticalVelocity, player.HorizontalVelocity);
                            //player.HorizontalVelocity += Math.Cos(player.Direction) * velocityIncrement / 2.0;
                            //player.VerticalVelocity += Math.Sin(player.Direction) * velocityIncrement / 2.0;

                            //if (player.HorizontalVelocity > 0)
                            //    player.HorizontalVelocity -= Math.Cos(player.Direction) * velocityIncrement / 2.0;
                            //else if (player.HorizontalVelocity < 0)
                            //    player.HorizontalVelocity += Math.Cos(player.Direction) * velocityIncrement / 2.0;

                            //if (player.VerticalVelocity > 0)
                            //    player.VerticalVelocity -= Math.Sin(player.Direction) * velocityIncrement / 2.0;
                            //else if (player.VerticalVelocity < 0)
                            //    player.VerticalVelocity += Math.Sin(player.Direction) * velocityIncrement / 2.0;

                            if (player.HorizontalVelocity > 0)
                                player.HorizontalVelocity -= velocityIncrement / 2.0;
                            else if (player.HorizontalVelocity < 0)
                                player.HorizontalVelocity += velocityIncrement / 2.0;

                            if (player.VerticalVelocity > 0)
                                player.VerticalVelocity -= velocityIncrement / 2.0;
                            else if (player.VerticalVelocity < 0)
                                player.VerticalVelocity += velocityIncrement / 2.0;
                        }
                        else
                        {
                            if (player.HorizontalVelocity > 0)
                                player.HorizontalVelocity -= velocityIncrement / 2.0;
                            else if (player.HorizontalVelocity < 0)
                                player.HorizontalVelocity += velocityIncrement / 2.0;

                            if (player.VerticalVelocity > 0)
                                player.VerticalVelocity -= velocityIncrement / 2.0;
                            else if (player.VerticalVelocity < 0)
                                player.VerticalVelocity += velocityIncrement / 2.0;
                            
                            //if (player.Velocity >= velocityIncrement)
                                //player.Velocity -= velocityIncrement / 2.0;
                        }
                    }
                }
            }

            //Maximum Velocity Control

            if (Math.Abs(player.HorizontalVelocity) > maxVelocity)
            {
                player.HorizontalVelocity = maxVelocity * GetSign(player.HorizontalVelocity);
            }

            if (Math.Abs(player.VerticalVelocity) > maxVelocity)
            {
                player.VerticalVelocity = maxVelocity * GetSign(player.VerticalVelocity);
            }

            ////Broken
            //if (player.Velocity > maxVelocity && !realisticSpaceMovement)
            //{
            //    int horizontalVelocitySign = GetSign(player.HorizontalVelocity);
            //    int verticalVelocitySign = GetSign(player.VerticalVelocity);

            //    player.HorizontalVelocity = Math.Cos(player.Direction) * maxVelocity;
            //    player.VerticalVelocity = Math.Sin(player.Direction) * maxVelocity;

            //    if (GetSign(player.HorizontalVelocity) != horizontalVelocitySign)
            //        player.HorizontalVelocity *= -1;
            //    if (GetSign(player.VerticalVelocity) != verticalVelocitySign)
            //        player.VerticalVelocity *= -1;

            //}

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

        #region Helper Methods

        private int GetSign(double value)
        {
            if (value < 0)
                return -1;
            if (value == 0)
                return 0;
            else
                return 1;
        }

        #endregion

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            DrawDebugWindow(spriteBatch, Color.Red);
            player.Draw(spriteBatch, spritesheet);
            //spriteBatch.Draw(spritesheet, new Rectangle(32,32,32,32), new Rectangle(0,0,32,32), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //precondition: spriteBatch.Begin() has been called
        protected void DrawDebugWindow(SpriteBatch spriteBatch, Color textColor)
        {
            if (debugVisible)
            {
                List<string> debugLines = new List<string>();
                debugLines.Add("Player Ship Coordinates: (" + player.X + ", " + player.Y + ")");
                debugLines.Add("xVelocity: " + player.HorizontalVelocity + "   yVelocity: " + player.VerticalVelocity);
                debugLines.Add("Rotational Velocity: " + player.RotationalVelocity);

                Vector2 offset = new Vector2(4, 2);
                foreach (string line in debugLines)
                {
                    spriteBatch.DrawString(debugFont, line, offset, textColor);
                    offset.Y += debugFont.MeasureString(line).Y - 1;
                }
            }
        }
    }
}
