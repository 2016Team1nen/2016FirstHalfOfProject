using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zombie.Active
{
    class Beam
    {
        private string name;
        private Vector2 position;
        private Vector2 velocity;
        private Vector2 size;
        private float range;

        private float start;    //発射した時のX座標
        private Vector2 startPos;
        private int rf;            //プレイヤーの向き
        private bool beamType;

        //プロパティー
        public Vector2 Position
        {
            get { return position; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
        }

        public Vector2 Size
        {
            get { return size; }
        }

        public float DistanceTravelled
        {
            get { return Vector2.Distance(startPos, position); }
        }

        public float Range
        {
            get { return range; }
        }

        public float Start
        {
            get { return start; }
            set { start = value; }
        }

        public int RightFacing
        {
            get { return rf; }
            set { rf = value; }
        }

        //コンストラクタ
        public Beam(Vector2 position, Vector2 size, Vector2 velocity, bool beamType)
        {
            name = "beam";
            this.beamType = beamType;
            this.position = position;
            this.velocity = velocity;
            this.size = size;
            startPos = position;
            range = 500;
        }

        //メソッド
        public void Update()
        {
            velocity.X += rf;
            position += velocity;
        }

        //get
        public bool GetBeamType()
        {
            return beamType;
        }


        public float GetStart()
        {
            return start;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetSize()
        {
            return size;
        }




        //change
        public void ChangeStart(float start, int rf) {
            this.start = start;
            this.rf = rf;
        }


        public void Draw(Renderer renderer, bool cb)
        {
            renderer.Draw(name, position, cb);
        }

    }
}
