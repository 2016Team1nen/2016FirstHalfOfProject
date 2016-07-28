using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Zombie.Device
{
    public class Camera
    {
        Vector2 position;
        Matrix viewMatrix;
        Viewport view;
        float scale = 1.0f;

        public Matrix ViewMatrix { get { return viewMatrix; } }

        public void Update(Vector2 playerPosition) {
            position.X = ((playerPosition.X) - (view.Width / 2) / scale);
            position.Y = ((playerPosition.Y - 600) - (view.Height / 2) / scale);

            if (position.X < -5000) { position.X = -5000; }
            if (position.Y < -0) { position.Y = -0; }

            //scaleはズームの量
            if (Keyboard.GetState().IsKeyDown(Keys.W)) { scale += 0.01f; }
            else if (Keyboard.GetState().IsKeyDown(Keys.S)) { scale -= 0.01f; }

            viewMatrix = Matrix.CreateTranslation(new Vector3(new Vector2(500,0) - position, 0)) * Matrix.CreateScale(scale);
        }
    }
}
