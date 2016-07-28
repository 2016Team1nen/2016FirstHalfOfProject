using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Device;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zombie.Sceen
{
    class Staffrole : ISceen
    {
        
        private bool isEnd;
        private InputState input;
        private Sound sound;

        public Staffrole(DeviceManager deviceManager)
        {
            input = deviceManager.GetInputState();
            sound = deviceManager.GetSound();
            Initialize();
        }

        public void Initialize() {
            isEnd = false;
        }

        public void Update(GameTime gameTime) {
            //sound.PlayeBGM("GameOverChords");
            input.UpdateKey(Keyboard.GetState());
            if (input.IsKeyDown(Keys.Enter))
            {
                sound.PlaySE("endingse");
                sound.StopBGM();
                isEnd = true;
            }
        }
        public void Draw(Renderer renderer) {
            renderer.Begin();
            //renderer.DrawTextureW("ending", Vector2.Zero);
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
