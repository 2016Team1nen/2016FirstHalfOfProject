using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zombie
{
    class InputState
    {

        public InputState() { }

        public Vector2 Move(KeyboardState keyState , Vector2 velocity)
        {
            velocity.X = 0;

            if (keyState.IsKeyDown(Keys.A))
            {
                velocity.X -= 1;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                velocity.X += 1;
            }

            return velocity;
        }

        


    }
}
