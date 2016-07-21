using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Zombie.Active;
using Zombie.Device;

namespace Zombie
{
    class Player : Character
    {
        private InputState input;
        private int rl;
        private bool beamType;
        private List<Beam> beamR;
        private List<Beam> beamL;

        public Player(string name, int hp, Vector2 position, Vector2 size, Vector2 velocity)
            : base(name, hp, position, size, velocity) {
                input = new InputState();
                beamL = new List<Beam>();
                beamR = new List<Beam>();
                rl = 1;
                beamType = true;
        }

        public override void Update(GameTime gameTime) {
            input.UpdateKey(Keyboard.GetState());
            Jump();
            Falling();
            Move();
            ChangeBeam();
            Shoot();
        }


        private void Shoot() {
            if (input.IsKeyDown(Keys.Space))
            {
                if (rl > 0) {
                    beamR.Add(new Beam("beam", 1, (position + new Vector2(size.X,0)), new Vector2(32, 32), Vector2.Zero, beamType, rl));
                }
                else {
                    beamL.Add(new Beam("beam", 1, (position + new Vector2(-16, 0)), new Vector2(32, 32), Vector2.Zero, beamType, rl));
                }
            }

            beamR.RemoveAll(b => b.GetStart() < (b.GetPosition().X - 500));
            beamL.RemoveAll(b => b.GetStart() > (b.GetPosition().X + 500));
        }
        
        
        
        //csv


        //beamの種類を切り替える true:水色、false:ピンク
        private void ChangeBeam() {
            if (input.IsKeyDown(Keys.Q))
            { beamType = !beamType; }
        }

        //rl : right=1,left=-1
        private void Move() {
            float speed = 5;
            if (input.IsA()) { velocity.X = -1; rl = -1; }
            else if (input.IsD()) { velocity.X = 1; rl = 1; }
            else { velocity.X = 0;  }
            position.X += velocity.X * speed;
        }

        private void Jump() {
            if (velocity.Y == 0 && input.IsW()) {
                velocity.Y -= 25.6f;
            }
            position.Y += velocity.Y;
        }

        public int GetRL() { return rl; }
        public List<Beam> GetBeamL() { return beamL; }
        public List<Beam> GetBeamR() { return beamR; }
    }
}
