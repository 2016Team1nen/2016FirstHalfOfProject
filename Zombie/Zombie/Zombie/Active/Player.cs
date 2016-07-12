using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Zombie.Active;

namespace Zombie
{
    class Player : Character
    {
        private InputState inputState = new InputState();
        

        public Player(string name, int hp, Vector2 position, Vector2 size, Vector2 velocity)
            : base(name, hp, position, size, velocity) {
        }


        public void Shoot(List<Character> beamR, List<Character>beamL, int rf) {
            inputState.UpdateKey(Keyboard.GetState());

            if (rf > 0)
            {
                if (inputState.IsKeyDown(Keys.Space))
                {
                    Vector2 start = position + new Vector2(64, 10);
                    beamR.Add(new Beam("beam", 1, start, new Vector2(32, 32), Vector2.Zero));
                    ((Beam)beamR[beamR.Count - 1]).ChangeStart(position.X,rf);
                }
            }

            else {
                if (inputState.IsKeyDown(Keys.Space))
                {
                    Vector2 start = position + new Vector2(-32, 10);
                    beamL.Add(new Beam("beam", 1, start, new Vector2(32, 32), Vector2.Zero));
                    ((Beam)beamL[beamL.Count - 1]).ChangeStart(position.X,rf);
                }                
            }

            foreach (var b in beamR)
            {
                if (((Beam)b).GetStart() < (b.GetPosition().X - 500))
                {
                    beamR.Remove(b);
                    break;
                }
            }

            foreach (var b in beamL)
            {
                if (((Beam)b).GetStart() > (b.GetPosition().X + 500))
                {
                    beamL.Remove(b);
                    break;
                }
            }

        }






        public override void Update() {
            Move();
            Jump();
            base.Falling();
        }

        private void Move()
        {
            float speed = 5;

            velocity = inputState.Move(Keyboard.GetState(), velocity);
            position.X += velocity.X * speed;
            
        }

        private void Jump()  
        {
            if (velocity.Y == 0 && Keyboard.GetState().IsKeyDown(Keys.W))    
                {
                    velocity.Y -= 20.1f;
                }
        }

        //csv

        

    }
}
