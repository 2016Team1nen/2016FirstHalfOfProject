using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zombie.Active
{
    class Beam:ActorObject
    {
        private float start;    //発射した時のX座標
        private int rl;            //プレイヤーの向き
        private bool beamType;

        public Beam(string name, Vector2 position, Vector2 size, Vector2 velocity, bool beamType, int rl)
            :base(name, position, velocity, size)
        {
            this.rl = rl;
            this.beamType = beamType;
            this.start = position.X;
        }

        public override void Update(GameTime gameTime)
        {
            velocity.X += rl;
            position += velocity;
        }

        //get
        public bool GetBeamType() { return beamType; }
        public float GetStart() { return start; }

        //change
        public void ChangeRL(int rl) { this.rl = rl; }

        public void Draw(Renderer renderer, bool cb) { renderer.Draw(name, position, cb); }
    }
}
