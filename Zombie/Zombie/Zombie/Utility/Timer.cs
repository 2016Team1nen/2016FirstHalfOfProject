using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombie.Utility
{
    class Timer
    {
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
    }
}
