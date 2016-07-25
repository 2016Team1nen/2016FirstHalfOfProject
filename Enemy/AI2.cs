using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Zombie;


namespace teamwork1.Enemy
{
    abstract　class AI2
    {
        protected Vector2 position;
        public AI2()
        {
            position = Vector2.Zero;
        }
        public abstract Vector2 Thinks(Enemy enemy);
    }
}
