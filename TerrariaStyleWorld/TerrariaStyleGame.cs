using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TerrariaStyleWorld.Graphics;

namespace TerrariaStyleWorld
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TerrariaStyleGame : Game
    {
        struct RenderDebugInfo
        {
            public int numTilesDrawn;
            public int numTilesTotal;
        }

        private const int MAX_FPS = 300;

        private const float CAMERAMOVESPEED = 1000f;// measured in pixels
        private const float CAMERA_ZOOM = 1.0f;
        private const float CAMERA_ZOOM_FAKE = 2.0f;// remove this later

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Graphics Settings
        private readonly int[] mWindowBounds = { 1600, 900};
        RenderDebugInfo mRenderDebugInfo;
        Texture2D mGridTexture;

        // Game camera for rendering the desired area of the world
        private Camera mCamera;

        // font for sebug text
        SpriteFont mDebugFont;

        // World settings
        private Point mWorldBounds = new Point(500, 500);
        private World mWorld;

        public TerrariaStyleGame()
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

            graphics.PreferredBackBufferWidth = mWindowBounds[0];
            graphics.PreferredBackBufferHeight = mWindowBounds[1];
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.ApplyChanges();

            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 1000 / MAX_FPS);

            InputManager.Init();
            FrameCounter.Init();

            // initializing the camera with default values
            //mCamera.Position = new Vector2(0, 0);
            //mCamera.Zoom = CAMERA_ZOOM;
            mCamera = new Camera(new Vector2(0, 0), GraphicsDevice.Viewport.Bounds.Size, CAMERA_ZOOM, CAMERA_ZOOM_FAKE);

            mWorld = new World(mWorldBounds);
            mWorld.Generate(out mRenderDebugInfo.numTilesTotal);

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
            mDebugFont = Content.Load<SpriteFont>(@"Fonts\courier");
            Tile.TileTextureAtlas = Content.Load<Texture2D>(@"Textures\Atlas\Terrain");

            mGridTexture = Content.Load<Texture2D>(@"Textures\WorldGrid");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            InputManager.Update();

            Vector2 camMoveDir = Vector2.Zero;

            if (InputManager.GetKey(Keys.W))
            {
                camMoveDir.Y += 1;
            }
            if (InputManager.GetKey(Keys.S))
            {
                camMoveDir.Y -= 1;
            }
            if (InputManager.GetKey(Keys.D))
            {
                camMoveDir.X += 1;
            }
            if (InputManager.GetKey(Keys.A))
            {
                camMoveDir.X -= 1;
            }

            if (!camMoveDir.Length().Equals(0))
            {
                camMoveDir.Normalize();
                float deltaTime = gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
                mCamera.Move(camMoveDir, CAMERAMOVESPEED * deltaTime);

            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // resize the game camera

            // TODO: Add your drawing code here
            DrawGame(gameTime);
            
            DrawHud(gameTime);

            base.Draw(gameTime);
        }

        private void DrawGame(GameTime gameTime)
        {
            FrameCounter.onDraw((float)gameTime.ElapsedGameTime.Milliseconds/1000);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, mCamera.getView());

            mWorld.Draw(spriteBatch, mCamera, graphics.GraphicsDevice.Viewport.Bounds, ref mRenderDebugInfo.numTilesDrawn);

            Rectangle gridRect = new Rectangle(-128, -128, 256, 256);
            spriteBatch.Draw(mGridTexture, gridRect, Color.White);

            spriteBatch.End();
        }

        private void DrawHud(GameTime gameTime)
        {
            spriteBatch.Begin();
            float fps = FrameCounter.GetAverageFramerate();
            spriteBatch.DrawString(mDebugFont, "FPS: " + fps.ToString("N0"), new Vector2(10, 10), Color.White);
            string tileDebug = "Tiles Drawn: " + mRenderDebugInfo.numTilesDrawn + " / " + mRenderDebugInfo.numTilesTotal + " ( " + ((float)mRenderDebugInfo.numTilesDrawn / (float)mRenderDebugInfo.numTilesTotal * 100.0f).ToString("N0") + " % )";
            spriteBatch.DrawString(mDebugFont, tileDebug, new Vector2(10, 25), Color.White);
            spriteBatch.End();
        }
    }
}
