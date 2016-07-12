using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zombie
{
    class InputState
    {
        private KeyboardState currentKey;
        private KeyboardState previousKey;
        private int checkRF;

        public InputState() { }

        public Vector2 Move(KeyboardState keyState , Vector2 velocity)
        {
            velocity.X = 0;

            if (keyState.IsKeyDown(Keys.A))
            {
                velocity.X -= 1;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                velocity.X += 1;
            }

            return velocity;
        }

        //向きチェック right=1,left=-1
        public int CheckRF(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.D)) {
                checkRF = 1;
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                checkRF = -1;
            }
            return checkRF;
        }


        //Keyの状態をチェックするメソッド
        public void UpdateKey(KeyboardState keyState)
        {
            previousKey = currentKey;
            currentKey = keyState;
        }
        public bool IsKeyDown(Keys key)
        {
            bool current = currentKey.IsKeyDown(key);
            bool previous = previousKey.IsKeyDown(key);
            return current && !previous;
        }



    }
}
