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

        //四角い判定
        public bool Update(Vector2 active, Vector2 passive, Vector2 aSize, Vector2 pSize) {
            Rectangle aRect = new Rectangle((int)active.X, (int)active.Y, (int)aSize.X, (int)aSize.Y);
            Rectangle pRect = new Rectangle((int)passive.X, (int)passive.Y, (int)pSize.X, (int)pSize.Y);
            return aRect.Intersects(pRect);
        }

        //windowsとのあたり判定
        public Vector2 Collision(Vector2 active) {
            active = Vector2.Clamp(active, new Vector2(500, -500), new Vector2(9000 - 64, Screen.screenHeight - 64));
            return active;
        }
    }
}
