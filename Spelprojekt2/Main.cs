using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Spelprojekt2.Enemies;
using Spelprojekt2.UI;
using Spelprojekt2.Towers;

namespace Spelprojekt2
{
    public class Main : Game
    {
        public static Main instance;

        public Level level;

        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch { get; private set; }

        RenderTarget2D scene;

        public Main()
        {
            instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = Global.ScreenWidth;
            graphics.PreferredBackBufferHeight = Global.ScreenHeight;
            IsMouseVisible = true;
            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            //graphics.ToggleFullScreen();
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
            level = Level.GenerateExampleLevel();
            level.LoadEnemies();
            Global.Load();

            PresentationParameters pp = GraphicsDevice.PresentationParameters;
            scene = new RenderTarget2D(graphics.GraphicsDevice, 480, 270, false, SurfaceFormat.Color, DepthFormat.None, pp.MultiSampleCount, RenderTargetUsage.DiscardContents);

        }

        protected override void UnloadContent()
        {
            level.SaveGame();
        }

        protected override void Update(GameTime gameTime)
        {
            Input.BeginInput();
            if (Input.Kstate.IsKeyDown(Keys.Escape))
                Exit();

            Global.Update(gameTime);

            if (Input.Pressed(Keys.F2))
            { // Ta skärmbild
                scene.SaveAsPng(new FileStream("screenshot.png", FileMode.Create), Global.ScreenWidth, Global.ScreenHeight);
            }

            base.Update(gameTime);
            Input.EndInput();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(scene);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            level.Draw();

            foreach (Tower tower in Global.PlacedTowers)
            {
                tower.Draw();
            }
            GUI.towerHeld?.Draw();

            spriteBatch.End();

            Tower.DrawRange();
            Enemy.DrawHPBars();
            Global.DrawEffects();

            // Rita GUI
            spriteBatch.Begin(SpriteSortMode.Immediate, samplerState: SamplerState.PointClamp);
            GUI.Draw();
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(scene, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
