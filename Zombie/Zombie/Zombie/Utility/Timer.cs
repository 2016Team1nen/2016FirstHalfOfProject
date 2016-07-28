using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
<<<<<<< HEAD
=======
using Microsoft.Xna.Framework;
>>>>>>> origin/you

namespace Zombie.Utility
{
    class Timer
    {
<<<<<<< HEAD
        private float curretTime;
        private float limitTime;

        public Timer()
        {
            limitTime = 60.0f;
            Initialize();
        }

        public Timer(float second)
        {
            limitTime = 60.0f * second;
            Initialize();
        }

        public void Initialize()
        {
            curretTime = limitTime;
        }

        public void Update()
        {
            curretTime -= 1.0f;
            curretTime = Math.Max(curretTime, 0);
        }

        public float Now()
        {
            return curretTime;
        }

        public bool IsTime()
        {
            return curretTime <= 0.0f;
        }

        public void Change(float limitTime)
        {
            this.limitTime = limitTime;
            Initialize();
        }
=======
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
>>>>>>> origin/you
    }
}
