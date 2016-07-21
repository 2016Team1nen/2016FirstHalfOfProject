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
        private Select select;
        private int s;

        public Title(DeviceManager deviceManager) {
            input = deviceManager.GetInputState();
            sound = deviceManager.GetSound();
            select = deviceManager.GetSelect();
            isEnd = false;
        }

        public void Initialize() {
            isEnd = false;
        }
        public void Update(GameTime gameTime) {
            sound.PlayeBGM("titlebgm");
            select.SelectT();
            s = select.GetSelect();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && s == 0) {
                sound.PlaySE("titlese");
                sound.StopBGM();
                isEnd = true;
            }
        }
        public void Draw(Renderer renderer) {
            renderer.Begin();
            renderer.DrawTextureW("title", Vector2.Zero);
            if (s == 0) 
            { renderer.DrawTextureG("gamestart", new Vector2(430, 400)); }
            else if (s == 1) 
            { renderer.DrawTextureG("stuffrole", new Vector2(430, 490)); }
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
