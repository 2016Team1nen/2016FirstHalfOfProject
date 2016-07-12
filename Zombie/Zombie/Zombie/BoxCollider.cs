using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zombie
{
    class BoxCollider
    {
        public Vector2 position;
        public Vector2 size;

        BoxCollider(Vector2 position, Vector2 size) {
            this.position = position;
            this.size = size;
        }
    }
}
