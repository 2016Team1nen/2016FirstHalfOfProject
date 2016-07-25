using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Zombie;

namespace teamwork1.Enemy
{
    abstract class AI
    {
        protected Vector2 position;
        public AI()
        {
            position = Vector2.Zero;
        }
        public abstract Vector2 Think(Enemy enemy);

       

    }
}
