using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Zombie.Active;

namespace Zombie
{
    class InputState
    {
        private KeyboardState currentKey;
        private KeyboardState previousKey;

        public InputState() {
        }

        //押しっぱなし
        public bool IsDown(Keys key) { return Keyboard.GetState().IsKeyDown(key); }

        //Keyの状態をチェックするメソッド
        public void UpdateKey(KeyboardState keyState)
        {
            previousKey = currentKey;
            currentKey = keyState;
        }

        //一回押した
        public bool IsKeyDown(Keys key)
        {
            bool current = currentKey.IsKeyDown(key);
            bool previous = previousKey.IsKeyDown(key);
            return current && !previous;
        }

    }
}
