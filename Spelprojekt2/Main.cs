using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.SplineFlower.Content;

namespace Spelprojekt2
{
    public class Main : Game
    {
        public static Main instance;

        public int ScreenWidth { get; private set; } = 1920;
        public int GameWidth { get; private set; } = 480;
        public int GameHeight { get; private set; } = 270;
        public int ScreenHeight { get; private set; } = 1080;

        public Level level;

        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch { get; private set; }

        RenderTarget2D scene;

        public List<Tower> placedTowers;

        Enemy enemy;

        public Main()
        {
            instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            IsMouseVisible = true;
            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Assets.Initialize(Content);
            DebugTextures.LoadTextures(GraphicsDevice);
            Setup.Initialize(GraphicsDevice);
            
            level = Level.GenerateExampleLevel();

            PresentationParameters pp = GraphicsDevice.PresentationParameters;
            scene = new RenderTarget2D(graphics.GraphicsDevice, 480, 270, false, SurfaceFormat.Color, DepthFormat.None, pp.MultiSampleCount, RenderTargetUsage.DiscardContents);

            placedTowers = new List<Tower>();
            placedTowers.Add(new GunTower(new Vector2(GameWidth / 2f, GameHeight / 2f)));

            enemy = new Enemy();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Tower tower in placedTowers)
            {
                tower.Update(gameTime);
            }
            

            enemy.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(scene);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (Tower tower in placedTowers)
            {
                tower.Draw(spriteBatch);
            }

            level.Draw();
            enemy.Draw();

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(scene, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
