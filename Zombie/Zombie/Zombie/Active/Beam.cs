using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zombie.Active
{
    class Beam : Character
    {
        private float start;
        int rf;

        public Beam(string name, int hp, Vector2 position, Vector2 size, Vector2 velocity)
            : base(name, hp, position, size, velocity) {
                name = "beam";                
        }

        public override void Update()
        {
            velocity.X += rf;
            position += velocity;
        }

        public float GetStart()
        {
            return start;
        }

        public void ChangeStart(float start, int rf) {
            this.start = start;
            this.rf = rf;
        }

        

    }
}
