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

        public bool IsUp(){ return Keyboard.GetState ().IsKeyDown(Keys.Up); } //上
        public bool IsDown() { return Keyboard.GetState().IsKeyDown(Keys.Down); } //下
        public bool IsA(){ return Keyboard.GetState ().IsKeyDown(Keys.A); } //左
        public bool IsD() { return Keyboard.GetState().IsKeyDown(Keys.D); } //右
        public bool IsW() { return Keyboard.GetState().IsKeyDown(Keys.W); } //ジャンプ
        public bool IsSpace() { return Keyboard.GetState().IsKeyDown(Keys.Space); } //シュート
        public bool IsQ() { return Keyboard.GetState().IsKeyDown(Keys.Q); } //弾の切り替え

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
