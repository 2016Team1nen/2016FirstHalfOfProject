using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Zombie.Device;

namespace Zombie.Sceen
{
    class Ending : ISceen
    {
        private bool isEnd;

        private InputState input;
        public Ending(DeviceManager deviceManager) {
            this.input = deviceManager.GetInputState();
        }

        public void Initialize() {
            isEnd = false;
        }
        public void Update(GameTime gameTime) {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                isEnd = true;
            }
        }
        public void Draw(Renderer renderer) {
            renderer.Begin();
            renderer.DrawTexture("ending", Vector2.Zero);
            renderer.End();
        }

        public bool IsEnd() {
            return isEnd;
        }
        public IsSceen Next() {
            Initialize();
            return IsSceen.TITLE;
        }

    }
}
