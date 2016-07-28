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
        private bool a;
        int x = 400, y = 490;

        public Title(DeviceManager deviceManager) {
            input = deviceManager.GetInputState();
            sound = deviceManager.GetSound();
            select = deviceManager.GetSelect();
            isEnd = false;
            a = true;
        }

        public void Initialize() {
            isEnd = false;
        }

        public void Update(GameTime gameTime) {
            sound.PlayeBGM("titlebgm");
            select.SelectT();
            s = select.GetSelect();
            input.UpdateKey(Keyboard.GetState());
            if (input.IsKeyDown(Keys.Enter) && s == 0) {
                sound.PlaySE("titlese");
                sound.StopBGM();
                isEnd = true;
            }
        }
        public void Draw(Renderer renderer) {
            renderer.Begin();
            renderer.DrawTextureW("title", Vector2.Zero);
            
            
            if (a){
                x++;
                y++;
                if (x >= 408) { a = false; }
            }
            else {
                x--;
                y--;
                if (x <= 393) { a = true; }
            }

            if (s == 0) {
                renderer.DrawTextureG("gamestart", new Vector2(430, x));
                renderer.DrawTextureW("staffcredits", new Vector2(365, 490)); 
            }
            else if (s == 1) {
                renderer.DrawTextureW("gamestart", new Vector2(430, 400));
                renderer.DrawTextureG("staffcredits", new Vector2(365, y));
            }
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
