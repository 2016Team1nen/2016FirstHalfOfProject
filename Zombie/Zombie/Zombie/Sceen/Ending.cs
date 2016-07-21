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
        private Sound sound;

        public Ending(DeviceManager deviceManager) {
            input = deviceManager.GetInputState();
            sound = deviceManager.GetSound();
        }

        public void Initialize() {
            isEnd = false;
        }
        public void Update(GameTime gameTime) {
            sound.PlayeBGM("endingbgm");
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                sound.PlaySE("endingse");
                sound.StopBGM();
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
