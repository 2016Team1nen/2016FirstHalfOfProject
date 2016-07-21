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
        private Sound sound;
        private bool isEnd;

        public Title(DeviceManager deviceManager)
        {
            input = deviceManager.GetInputState();
            sound = deviceManager.GetSound();
            isEnd = false;
        }

        public void Initialize() {
            isEnd = false;
        }
        public void Update(GameTime gameTime) {
            sound.PlayeBGM("titlebgm");

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                sound.PlaySE("titlese");
                sound.StopBGM();
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
