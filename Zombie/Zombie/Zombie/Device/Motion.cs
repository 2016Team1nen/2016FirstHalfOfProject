using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Utility;
using Microsoft.Xna.Framework;

namespace Zombie.Device
{
    class Motion
    {
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
            motionNumber += 1;
            if (range.IsOutOfRange(motionNumber))
            {
                motionNumber = range.First();
            }
        }

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
    }
}
