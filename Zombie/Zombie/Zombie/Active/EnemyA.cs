using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zombie
{
    class EnemyA : Character
    {
        private bool isPursue;

        public EnemyA(string name, int hp, Vector2 position, Vector2 size, Vector2 velocity)
            : base(name, hp, position, size, velocity) {
                isPursue = false;
        }


        //動き
        public void Move(Vector2 player)
        {
            Pursue(player);
            if (isPursue)
            {
                if (player.X - position.X > 0)
                {
                    if (velocity.X < 0)
                    {
                        velocity.X *= -1;
                    }
                }
                else
                {
                    if (velocity.X > 0)
                    {
                        velocity.X *= -1;
                    }
                }
            }
            
            else
            {
                if (position.X < 500)
                {
                    velocity.X *= -1;
                }
                if (position.X > 700)
                {
                    velocity.X *= -1;
                }
            }
            position += velocity;
        }

        

        //追いかけスイッチ
        public void Pursue(Vector2 player) {
            float dictance = (float)Math.Sqrt((player.X - position.X) * (player.X - position.X) + (player.Y - position.Y) * (player.Y - position.Y));

            if (dictance <= 200) {
                isPursue = true;
            }
        }





        //実装してない
        protected override void Move()
        {
        }

        public override void Update()
        {
        }

        //public override void Jump(Microsoft.Xna.Framework.Input.KeyboardState keyState, Vector2 floor){}
       
    }
}
