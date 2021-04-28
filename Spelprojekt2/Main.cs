﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.SplineFlower.Content;
using System.IO;

namespace Spelprojekt2
{
    public class Main : Game
    {
        public static Main instance;

        public Level level;

        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch { get; private set; }

        RenderTarget2D scene;

        Enemy enemy;

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
            Global.Load();
            Setup.Initialize(GraphicsDevice);
            
            level = Level.GenerateExampleLevel();

            PresentationParameters pp = GraphicsDevice.PresentationParameters;
            scene = new RenderTarget2D(graphics.GraphicsDevice, 480, 270, false, SurfaceFormat.Color, DepthFormat.None, pp.MultiSampleCount, RenderTargetUsage.DiscardContents);

            Global.placedTowers = new List<Tower>();
            Global.placedTowers.Add(new GunTower(new Vector2(Global.GameWidth / 2f, Global.GameHeight / 2f)));

            level.Enemies.Add(new Enemy());
            level.Enemies.Add(new Enemy() { t = 0.5f});
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            Input.BeginInput();
            if (Input.Kstate.IsKeyDown(Keys.Escape))
                Exit();

            Global.Update(gameTime);

            foreach (Tower tower in Global.placedTowers)
            {
                tower.Update(gameTime);
            }

            level.Update(gameTime);

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

            foreach (Tower tower in Global.placedTowers)
            {
                tower.Draw(spriteBatch);
            }

            level.Draw();
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
