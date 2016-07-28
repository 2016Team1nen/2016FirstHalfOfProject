using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
<<<<<<< HEAD
using Zombie.Utility;
using Microsoft.Xna.Framework;
=======
using Microsoft.Xna.Framework;
using Zombie.Device;
using Zombie.Utility;
>>>>>>> origin/you

namespace Zombie.Device
{
    class Motion
    {
<<<<<<< HEAD
        private Range range; //範囲
        private Timer timer; //モーション時間
        private int motionNumber; //モーション番号

        private Dictionary<int, Rectangle> rectangles =
            new Dictionary<int, Rectangle>();

        public Motion()
        {
            //何もしない
            Initialize(new Range(0, 0), new Timer());
        }
        public Motion(Range range, Timer timer)
        {
            Initialize(range, timer);
        }
        public void Initialize(Range range, Timer timer)
        {
            this.range = range;
            this.timer = timer;

            motionNumber = range.First();
        }

        public void Add(int index, Rectangle rect)
        {
            if (rectangles.ContainsKey(index))
            {
                return;
            }
            rectangles.Add(index, rect);
        }

        private void MotionUpdate()
        {
=======
        private Range range;
        private Timer timer;
        private int motionNumber;

        private Dictionary<int, Rectangle> rectangles = new Dictionary<int, Rectangle>();

        public Motion() { Initialize(new Range(0, 0), new Timer(0.5f)); }

        public Motion(Range range, Timer timer) { Initialize(range, timer); }

        public void Initialize(Range range, Timer timer) {
            this.range = range;
            this.timer = timer;
            motionNumber = range.First();
        }

        public void Add(int index, Rectangle rect) {
            if (rectangles.ContainsKey(index)) { return; }
            rectangles.Add(index, rect);
        }

        private void MotionUpdate() {
>>>>>>> origin/you
            motionNumber += 1;
            if (range.IsOutOfRange(motionNumber))
            {
                motionNumber = range.First();
            }
        }

<<<<<<< HEAD
        public void Update(GameTime gametime)
        {
            if (range.IsOutOfRange())
            {
                return; //変化なし
            }
            timer.Update();
            if (timer.IsTime())
            {
                timer.Initialize();
                MotionUpdate();
            }
        }

        public Rectangle DrawingRange()
        {
            return rectangles[motionNumber];
        }
=======
        public void Update(GameTime gameTime) {
            if (range.IsOutOfRange()) { return; }
            timer.Update();
            if (timer.IsTime()) {
                MotionUpdate();
                timer.Initialize();
            }
        }

        public Rectangle DrawingRange() { return rectangles[motionNumber]; }

>>>>>>> origin/you
    }
}
