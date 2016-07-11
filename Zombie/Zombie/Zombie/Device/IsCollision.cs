using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zombie.Device
{
    class IsCollision
    {
        public IsCollision() { }


        //四角い判定全般
        public bool Update(Vector2 active, Vector2 passive, Vector2 aSize, Vector2 pSize)
        {
            if (//左下から行く
              active.X + aSize.X >= passive.X + pSize.X && active.X + aSize.X <= (passive.X + pSize.X + 64) &&
              active.Y >= passive.Y + pSize.Y && active.Y <= (passive.Y + pSize.Y + 64))
            {
                return true;
            }
            if (//右上から行く
                active.X >= passive.X + pSize.X && active.X <= (passive.X + pSize.X + 64) &&
                active.Y + aSize.Y >= passive.Y + pSize.Y && active.Y + aSize.Y <= (passive.Y + pSize.Y + 64))
            {
                return true;
            }

            else
            {
                return false;
            }
        }


    }
}
