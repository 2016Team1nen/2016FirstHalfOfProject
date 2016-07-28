using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zombie.Utility
{
    class Timer
    {
        private float currentTime;  //今の時間
        private float lmitTime;     //制限時間
        private bool s;

        public Timer(float time) {
            lmitTime = time;
            currentTime = time;
        }

        public void Initialize() { currentTime = lmitTime; }

        public void Update() {
            s = false;
            currentTime -= 0.1f;
            if (currentTime <= 0) {
                currentTime = lmitTime;    //lmitTime
                s = true;
            }
        }

        public bool IsTime() { return s; }
    }
}
