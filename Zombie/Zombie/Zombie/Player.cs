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

        public Player(string name, int hp, Vector2 position, Vector2 size, Vector2 velocity)
            : base(name, hp, position, size, velocity) {
                this.size = position + new Vector2(64, 64);
        }


        public override void Update() {
            size = position + new Vector2(64, 64);
        }

        public override void Move()
        {
            float speed = 7;

            velocity = inputState.Move(Keyboard.GetState(), velocity);
            position.X += velocity.X * speed;
            position.Y += velocity.Y;
                
        }

        public void Jump(KeyboardState keyState, bool IsCollision)
        {
            if (position.Y < Screen.screenHeight - 64 && !IsCollision)
            {
                velocity.Y += 1;

            }
            else if ((keyState.IsKeyDown(Keys.W)) && IsCollision)
            {
                velocity.Y -= 20;

            }
            else
            {
                velocity.Y = 0;
            }
        }




        public bool IsBlock(Vector2 other)
        {

            if (size.X >= other.X && position.X <= other.X + 192) { 

                if (//左右から行く
                    size.Y > other.Y + 5 && position.Y < (other.Y + 59))
                {
                    velocity.X = 0;

                    if (size.X <= other.X + 10) {
                        position.X = other.X - 64;
                    }
                    if (position.X >= (other.X + 182)) {
                        position.X = other.X + 192;                        
                    }

                }


                if (//上下から行く
                    size.Y >= other.Y && position.Y <= (other.Y + 64) && size.X != other.X && position.X != other.X + 192)
                {
                    if (position.Y >= other.Y -54)
                    {
                        velocity.Y *= -1; 
                        }
                    else if (size.Y <= other.Y + 10) {
                        velocity.Y = 0;
                        position.Y = other.Y - 64;
                        Jump(Keyboard.GetState(), true);
                    }
                    
                }
            return true;
            }
            else
            {
                return false;
            }
        }




        public bool IsFloor(Vector2 other)
        {

            if (position.Y == other.Y && position.X <= other.X + 10) 
            {
                
                position.X = other.X - 64;
                
                Jump(Keyboard.GetState(), true);
                Move();
                return true;
            }


            else if( position.Y > other.Y - 64 ){
                velocity.Y = 0;
                position.Y = other.Y - 64;
                Move();
                Jump(Keyboard.GetState(), true);
                return true;
            }

            else if (position.Y == other.Y - 64) 
            {
                velocity.Y = 0;
                position.Y = other.Y - 64;
                Jump(Keyboard.GetState(), true);
                Move();
                return true;
            }


            else
            {
                Move();
                Jump(Keyboard.GetState(), false);
                return false;
            }
        }





    }
}
