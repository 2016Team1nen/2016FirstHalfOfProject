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


        public override void Move()
        {
            throw new NotImplementedException();
        }

        public void Move(Vector2 player)
        {
            this.size = position + new Vector2(64, 64);

            Pursue(player);
            

            if (isPursue == true)
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

        public override void Update() {
            size = position + new Vector2(64, 64);
        }

        //追いかけ
        public void Pursue(Vector2 player) {
            float dictance = (player.X - position.X) * (player.X - position.X) + (player.Y - position.Y) * (player.Y - position.Y);

            if (dictance <= 10000) {
                isPursue = true;
            }
        }


    }
}
