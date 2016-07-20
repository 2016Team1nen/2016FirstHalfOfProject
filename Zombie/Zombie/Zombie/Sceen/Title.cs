using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Zombie.Device;

namespace Zombie.Sceen
{
    class Title : ISceen
    {
        private InputState input;
        private bool isEnd;

        public Title(DeviceManager deviceManager)
        {
            this.input = deviceManager.GetInputState();
            isEnd = false;
        }

        public void Initialize() {
            isEnd = false;
        }
        public void Update(GameTime gameTime) {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                isEnd = true;
            }
        }
        public void Draw(Renderer renderer) {
            renderer.Begin();
            renderer.DrawTexture("title", Vector2.Zero);
            renderer.End();
        }
        public bool IsEnd() {
            return isEnd;
        }
        public IsSceen Next() {
            Initialize();
            return IsSceen.GAMEPLAY;
        }

    }
}
