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
        Texture2D background;
        GameImageSourceDirectory sourceDirectory;

        Song songy;
        
        KeyboardState previousKeyboard;
        MouseState previousMouse;
        GamePadState previousGamePad;

        Quadtree collisionQuadtree;

        //Options
        private bool realisticSpaceMovement;
        private bool automaticDeceleration;
        private bool mouseDirectionalControl;

        //Visual Graphics Options
        protected bool debugVisible;
        protected SpriteFont debugFont;

        protected bool staticBackground;

        List<ShardObject> shardObjects; //Probably won't be able to use as a ShardObject List
        Ship player;

        SoundPlayer soundPlayer;

        protected Camera camera;
        protected float zoomZ; 
        protected int zoomIO;
        protected bool isZooming;

        #region Database Fields
        //DatabaseConnection objConnect;
        //string conString;

        //DataSet ds;
        //DataRow dRow;

        //int MaxRows;
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

            previousGamePad = GamePad.GetState(PlayerIndex.One);
            previousKeyboard = Keyboard.GetState();
            previousMouse = Mouse.GetState();


            soundPlayer = new SoundPlayer();

            songy = Content.Load<Song>("Sounds/Musicmusic");
            MediaPlayer.Play(songy);

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
            //Visual
            debugVisible = true;
            staticBackground = true;
            //In-Game
            realisticSpaceMovement = false;
            automaticDeceleration = true;
            mouseDirectionalControl = false;

            

            shardObjects = new List<ShardObject>();

            player = new Ship(0, 0);
            player.Alignment = Shard.Alignment.GOOD;
            player.GunLevel = 1;
            player.MissileLevel = 1;

            camera = new Camera(player.Width / 2, player.Height / 2);
            camera.ScreenWidth = GraphicsDevice.Viewport.Width;
            camera.ScreenHeight = GraphicsDevice.Viewport.Height;
            zoomZ = 1;
            zoomIO = 2;
            isZooming = false;

            collisionQuadtree = new Quadtree(0, new Rectangle(0, 0, (int)camera.ScreenWidth, (int)camera.ScreenHeight));


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
            spritesheet = Content.Load<Texture2D>("Spritesheets//spritesheet_shard_i2");
            sourceDirectory = new GameImageSourceDirectory();
            sourceDirectory.LoadSourcesFromFile(@"Content/Spritesheets//spritesheet_shard_i2.txt");

            //Background Loading
            background = Content.Load<Texture2D>("Backgrounds//seamlessNebulaBackground");

            //Player Creation
            player.ImageSource = sourceDirectory.GetSourceRectangle("playerShip1_colored");
            player.Width = player.ImageSource.Width;
            player.Height = player.ImageSource.Height;
            player.Health = 10000;
            shardObjects.Add(player);

            soundPlayer.LoadSounds();

            //Add a bunch of debris for testing purposes
            int numDebris = 100;
            Random random = new Random();
            for (int i = 0; i < numDebris; i++)
            {
                //Debris debris = new Debris(random.Next(-GraphicsDevice.Viewport.Width,GraphicsDevice.Viewport.Width), random.Next(-GraphicsDevice.Viewport.Height,GraphicsDevice.Viewport.Height));
                Debris debris = new Debris(random.Next(GraphicsDevice.Viewport.Width), random.Next(GraphicsDevice.Viewport.Height));
                debris.Alignment = Shard.Alignment.NEUTRAL;
                debris.Health = 5;
                debris.Energy = 10;
                debris.Ore = 10;
                debris.Oxygen = 10;
                debris.Water = 10;
                debris.Direction = random.NextDouble() * Math.PI * 2;
                debris.ImageSource = sourceDirectory.GetSourceRectangle("smallAsteroid1_shaded");
                debris.Width = debris.ImageSource.Width;
                debris.Height = debris.ImageSource.Height;
                shardObjects.Add(debris);
            }

            //Add evil ships
            EnemyShip evil = new Follower(500,500);
            evil.GunLevel = 0;
            evil.Health = 10;
            evil.ImageSource = sourceDirectory.GetSourceRectangle("playerShip1_colored");
            evil.Width = evil.ImageSource.Width;
            evil.Height = evil.ImageSource.Height;
            shardObjects.Add(evil);

            //player.ImageSource = new Rectangle(64, 32, 32, 32);
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
            MouseState currentMouse = Mouse.GetState();

            // Exit Game
            if (currentGamePad.Buttons.Back == ButtonState.Pressed || currentKeyboard.IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here

            #region Player Rotation

            double maximumRotationalVelocity = Math.PI / 16.0;
            double rotationalVelocityIncrement = .001;
            double directionalChangeIncrement = .05;

            if (!mouseDirectionalControl)
            {
                if (realisticSpaceMovement)
                {
                    if (currentKeyboard.IsKeyDown(Keys.Left) || currentKeyboard.IsKeyDown(Keys.A))
                    {
                        if (player.RotationalVelocity > -maximumRotationalVelocity)
                            player.RotationalVelocity -= rotationalVelocityIncrement;
                    }
                    if (currentKeyboard.IsKeyDown(Keys.Right) || currentKeyboard.IsKeyDown(Keys.D))
                    {
                        if (player.RotationalVelocity < maximumRotationalVelocity)
                            player.RotationalVelocity += rotationalVelocityIncrement;
                    }
                }
                else
                {
                    if (currentKeyboard.IsKeyDown(Keys.Left) || currentKeyboard.IsKeyDown(Keys.A))
                        player.Direction -= directionalChangeIncrement;
                    if (currentKeyboard.IsKeyDown(Keys.Right) || currentKeyboard.IsKeyDown(Keys.D))
                        player.Direction += directionalChangeIncrement;

                    //player.Velocity = player.Velocity; //Looks weird, is necessary
                }
            }
            else
            {
                float unitx = 0;
                float unity = 0;
                TraceScreenCoord((int)currentMouse.X, (int)currentMouse.Y, out unitx, out unity);
                player.Direction = (float)Math.Atan2(unity - (player.Y + player.Height / 2), unitx - (player.X + player.Width / 2));
                //player.RotationalVelocity = 0;
                //player.Direction = Math.Atan2(currentMouse.Y - player.Y, currentMouse.X - player.X);
            }

            #endregion

            #region Player Movement and Deceleration Implementation

            double maxVelocity = player.GetMaxSpeed();
            double velocityIncrement = maxVelocity / 40.0;


            //Changes must be made directly to horizontal/vertical velocity of ship to simulate movement within space
            if (realisticSpaceMovement)
            {
                if (currentKeyboard.IsKeyDown(Keys.Up) || currentKeyboard.IsKeyDown(Keys.W))
                {
                    player.HorizontalVelocity += Math.Cos(player.Direction) * velocityIncrement;
                    player.VerticalVelocity += Math.Sin(player.Direction) * velocityIncrement;
                }
                else if (currentKeyboard.IsKeyDown(Keys.Down) || currentKeyboard.IsKeyDown(Keys.S))
                {
                    player.HorizontalVelocity += Math.Cos(player.Direction + Math.PI) * velocityIncrement;
                    player.VerticalVelocity += Math.Sin(player.Direction + Math.PI) * velocityIncrement;
                }
            }
            else //Ship Movement without velocity/rotation preservation
            {
                if (currentKeyboard.IsKeyDown(Keys.Up) || currentKeyboard.IsKeyDown(Keys.W))
                {
                    player.HorizontalVelocity += Math.Cos(player.Direction) * velocityIncrement;
                    player.VerticalVelocity += Math.Sin(player.Direction) * velocityIncrement;
                }
                else if (currentKeyboard.IsKeyDown(Keys.Down) || currentKeyboard.IsKeyDown(Keys.S)) // <-- Potentially Fixed Problems
                {
                    player.HorizontalVelocity -= Math.Cos(player.Direction) * velocityIncrement;
                    player.VerticalVelocity -= Math.Sin(player.Direction) * velocityIncrement;
                }

                //Strafing Movement, only works when Mouse Directional Control is enabled
                if (mouseDirectionalControl)
                {
                    double directionOfStrafe = player.Direction;
                    if ((currentKeyboard.IsKeyDown(Keys.Left) && !currentKeyboard.IsKeyDown(Keys.Right)) || (currentKeyboard.IsKeyDown(Keys.A) && !currentKeyboard.IsKeyDown(Keys.D)))
                        directionOfStrafe = player.Direction - MathHelper.PiOver2;
                    else if ((currentKeyboard.IsKeyDown(Keys.Right) && !currentKeyboard.IsKeyDown(Keys.Left)) || (currentKeyboard.IsKeyDown(Keys.D) && !currentKeyboard.IsKeyDown(Keys.A)))
                        directionOfStrafe = player.Direction + MathHelper.PiOver2;

                    if (directionOfStrafe != player.Direction)
                    {
                        player.HorizontalVelocity += Math.Cos(directionOfStrafe) * velocityIncrement;
                        player.VerticalVelocity += Math.Sin(directionOfStrafe) * velocityIncrement;
                    }
                }
            }

            //Automatic Deceleration of Player Ship Movement and Rotation
            if (automaticDeceleration)
            {
                //Player Rotation Deceleration
                if (!currentKeyboard.IsKeyDown(Keys.Left) && !currentKeyboard.IsKeyDown(Keys.Right) && !currentKeyboard.IsKeyDown(Keys.W) && !currentKeyboard.IsKeyDown(Keys.S))
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
                if (!currentKeyboard.IsKeyDown(Keys.Down) && !currentKeyboard.IsKeyDown(Keys.Up) && !currentKeyboard.IsKeyDown(Keys.W) && !currentKeyboard.IsKeyDown(Keys.S))
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

            #endregion

            //Shooting
            if ((currentMouse.LeftButton.Equals(ButtonState.Pressed)))
            {
                double temp = player.Direction;
                float unitx = 0;
                float unity = 0;
                TraceScreenCoord((int)currentMouse.X, (int)currentMouse.Y, out unitx, out unity);
                player.Direction = (float)Math.Atan2(unity - (player.Y + player.Height / 2), unitx - (player.X + player.Width / 2));
                player.Shoot(shardObjects, sourceDirectory);
                player.Direction = temp;
                //Projectile p = new Projectile((int)(player.ShipFront.X), (int)(player.ShipFront.Y));
                //p.ImageSource = sourceDirectory.GetSourceRectangle("shipBullet");
                //p.Width = p.ImageSource.Width;
                //p.Height = p.ImageSource.Height;
                //p.Direction = player.Direction;
                //p.RotationalVelocity = player.RotationalVelocity;
                //p.Velocity = 8;
                //shardObjects.Add(p);
            }

            //Vacuum Button
            if (currentKeyboard.IsKeyDown(Keys.V))
            {
                int vacuumWidth = (int)player.Width * 5;
                int vacuumHeight = (int)player.Height * 5;
                Rectangle vacuumBounds = new Rectangle((int)player.Center.X - vacuumWidth / 2, (int)player.Center.Y - vacuumHeight / 2, vacuumWidth, vacuumHeight);
                foreach (ShardObject so in shardObjects)
                {
                    if (so is Resource)
                    {
                        if (vacuumBounds.Intersects(so.GetBounds()))
                        {
                            so.PointTowards(player.Center);
                            so.Velocity = 4;
                        }
                    }
                }
            }

            if (currentMouse.ScrollWheelValue > previousMouse.ScrollWheelValue) //Mousewheel UP
            {
                isZooming = true;
                zoomIO = 1; //Sets I/O to zooming IN;
            }                     
            if (currentMouse.ScrollWheelValue < previousMouse.ScrollWheelValue) //Mousewheel DOWN
            {
                isZooming = true;
                zoomIO = 0; //Sets I/O to zooming OUT;
            }

            if (isZooming)
            {
                if (zoomIO == 1 && !(zoomZ >= 2))
                {
                    zoomZ += .1f;
                    camera.Zoom = zoomZ;
                }
                if (zoomIO == 0 && !(zoomZ <= 1))
                {
                    zoomZ -= .1f;
                    camera.Zoom = zoomZ;
                }
                if (zoomZ >= 2 && zoomIO == 1)
                {
                    isZooming = false;
                }
                if (zoomZ <= 1 && zoomIO == 0)
                {
                    isZooming = false;
                }
            }


            //player.Update(new List<GameObject>(), gameTime);

            //Throw all ShardObjects into a Quadtree for collision optimization purposes
            collisionQuadtree.Clear();
            foreach (ShardObject so in shardObjects)
            {
                if(camera.ScreenContains(so.GetBounds()))
                    collisionQuadtree.Insert(so);
            }

            //Update all ShardObjects using potentially colliding objects
            List<ShardObject> potentialCollisions = new List<ShardObject>();
            for (int i = 0; i < shardObjects.Count; i++ )
            {
                ShardObject so = shardObjects[i];
                potentialCollisions.Clear();
                collisionQuadtree.Retrieve(potentialCollisions, so);
                if (so is EnemyShip)
                {
                    ((EnemyShip)so).ProcessPlayer(player);
                    ((EnemyShip)so).Shoot(shardObjects, sourceDirectory);
                }
                if (so is Debris)
                    ((Debris)so).checkCollision(potentialCollisions);
                so.Update(potentialCollisions, gameTime);
            }

            //Remove ShardObjects declared invalid after the last update cycle
            for (int i = 0; i < shardObjects.Count; i++)
            {
                if (!shardObjects[i].IsValid())
                {
                    shardObjects[i].Destroy(shardObjects, sourceDirectory);
                    shardObjects.RemoveAt(i);
                }
            }

            //    if (player.X > GraphicsDevice.Viewport.Width)
            //        player.X = 0;
            //if (player.X + player.Width < 0)
            //    player.X = GraphicsDevice.Viewport.Width;
            //if (player.Y > GraphicsDevice.Viewport.Height)
            //    player.Y = 0;
            //if (player.Y + player.Height < 0)
            //    player.Y = GraphicsDevice.Viewport.Height;

            camera.SetPosition((float)player.X, (float)player.Y, 0);

            //Update Previous States
            
            previousGamePad = currentGamePad;
            previousKeyboard = currentKeyboard;
            previousMouse = currentMouse;

            base.Update(gameTime);
        }

        private void TraceScreenCoord(int x, int y, out float unitx, out float unity)
        {
            //Convert the pixel coordinate to units
            Matrix inv = Matrix.Invert(camera.GetViewMatrix());
            Vector2 pixel = new Vector2(x,y);
            Vector2 unit = Vector2.Transform(pixel, inv);
            unitx = unit.X + 0.5f;
            unity = unit.Y + 0.5f;
        }

        protected Vector2 ScreenToWorld(Vector2 coordinate)
        {
            Vector2 correctedCoords = new Vector2(0,0);
            return correctedCoords;
        }

        protected Vector2 WorldToScreen(Vector2 coordinate)
        {
            Vector2 correctedCoords = new Vector2(0, 0);
            return correctedCoords;
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

            //Draw the Background
            if(staticBackground)
                spriteBatch.Begin();
            else
                spriteBatch.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, camera.GetViewMatrix());
            spriteBatch.Draw(background, background.Bounds, Color.White);
            spriteBatch.End();

            //Draw all ShardObjects
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, camera.GetViewMatrix());
            foreach(ShardObject so in shardObjects)
            {
                so.Draw(spriteBatch, spritesheet);
            }
            spriteBatch.End();

            //Draw the Debug Window
            spriteBatch.Begin();
            DrawDebugWindow(spriteBatch, Color.Red);
            //spriteBatch.Draw(spritesheet, new Rectangle(32,32,32,32), new Rectangle(0,0,32,32), Color.White);
            spriteBatch.End();

            //Draw the Player
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, camera.GetViewMatrix());
            player.Draw(spriteBatch, spritesheet);
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
                debugLines.Add("Resources: " + player.Energy + " " + player.Ore + " " + player.Oxygen + " " + player.Water);
                //debugLines.Add("xVelocity: " + player.HorizontalVelocity + "   yVelocity: " + player.VerticalVelocity);
                //debugLines.Add("Rotational Velocity: " + player.RotationalVelocity);
                debugLines.Add("ShardObject Size: " + shardObjects.Count);

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
