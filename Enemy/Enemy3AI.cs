using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using teamwork1.Enemy;
using Zombie;

namespace teamwork1.Enemy
{
    
    class Enemy3AI:AI
    {
        private Character other;
       
        private Vector2 firstPosition;
        
        public Enemy3AI(Character other, Vector2 firstPosition)
        {
            this.other = other;
            this.firstPosition = firstPosition;
        }
        public override Vector2 Think(Enemy enemy)
        {
            enemy.SetPosition(ref position);


            Vector2 otherPosition = Vector2.Zero;
            other.SetPosition(ref otherPosition);

            Vector2 velocity = otherPosition - position;

            if (velocity == Vector2.Zero) { return position; }

            if (velocity.Length() <= 300)
            {
                velocity.Normalize();

                position += velocity * 3f;

            }


            if (velocity.Length() >= 300)
            {
                //position = firstPosition;

                velocity.Normalize();
                velocity = position - firstPosition;

                position -= velocity*0.03f;
            }


            return position;
        }

    }
}
