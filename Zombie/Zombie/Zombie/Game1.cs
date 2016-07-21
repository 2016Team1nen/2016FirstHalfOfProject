using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Zombie.Device;
using Zombie.Active;
using Zombie.Sceen;

namespace Zombie
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private DeviceManager deviceManager;
        private SceenManager sceenManager;
        private Renderer renderer;
        private Sound sound;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = Screen.screenHeight;
            graphics.PreferredBackBufferWidth = Screen.screenWidth;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            deviceManager = new DeviceManager(Content, GraphicsDevice);
            renderer = deviceManager.GetRenderer();
            sound = deviceManager.GetSound();

            sceenManager = new SceenManager();
            sceenManager.Add(IsSceen.TITLE, new Title(deviceManager));

            ISceen gamePlay = new GamePlay(deviceManager);
            sceenManager.Add(Sceen.IsSceen.GAMEPLAY, gamePlay);
            sceenManager.Add(Sceen.IsSceen.ENDING, new Ending(deviceManager));

            sceenManager.Change(Sceen.IsSceen.TITLE);

            base.Window.Title = "Zombie";
            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            renderer.LoadTexture("player");
            renderer.LoadTexture("enemy");
            renderer.LoadTexture("life");
            renderer.LoadTexture("block");
            renderer.LoadTexture("beam");
            renderer.LoadTexture("title");
            renderer.LoadTexture("ending");
            sound.LoadBGM("titlebgm");
            sound.LoadBGM("gameplaybgm");
            sound.LoadBGM("endingbgm");
            sound.LoadSE("titlese");
            sound.LoadSE("gameplayse");
            sound.LoadSE("endingse");

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            renderer.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape)) { this.Exit(); }

            // TODO: Add your update logic here
            deviceManager.Update(gameTime);
            sceenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            sceenManager.Draw(renderer);

            base.Draw(gameTime);
        }
    }
}
