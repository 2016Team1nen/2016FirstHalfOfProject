using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zombie
{
    class Player : Character
    {
        private InputState inputState = new InputState();
        private KeyboardState currentKey;
        private KeyboardState previousKey;


        public Player(string name, int hp, Vector2 position, Vector2 size, Vector2 velocity)
            : base(name, hp, position, size, velocity) {
        }


        public override void Update() {
            Move();
        }

        protected override void Move()
        {
            float speed = 5;

            velocity = inputState.Move(Keyboard.GetState(), velocity);
            position.X += velocity.X * speed;
            
        }

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

        public void Jump(KeyboardState keyState)    //, Vector2 p, bool b 
        {
            if (velocity.Y == 0 && keyState.IsKeyDown(Keys.W))    
                {
                    velocity.Y -= 20.1f;
                }
        }

        //csv

        public void IsEnemy(){
            if (IsDirection().X < 0)
            { 
                position.X += 150;
            }

            else
            {
                position.X -= 150;
            }
        }




    }
}
