using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Zombie.Sceen
{
    public class Camera
    {
        Vector2 position;
        Matrix viewMatrix;
        Viewport view;
        float scale = 1.0f;

        public Matrix ViewMatrix
        {
            get { return viewMatrix; }
        }

        public int ScreenWidth
        {
            get { return GraphicsDeviceManager.DefaultBackBufferWidth; }
        }
        public int ScreenHeight
        {
            get { return GraphicsDeviceManager.DefaultBackBufferHeight; }
        }

        public void Update(Vector2 playerPosition)
        {
            //X - 300 の値をいじると位置をずらせます。 / 2 のところはよくわかりません。

            //とりあえずのズーム版↓
            position.X = ((playerPosition.X - 300) - (view.Width / 2) / scale);
            position.Y = ((playerPosition.Y - 600) - (view.Height / 2) / scale);
            //ズームなし下
            //position.X = playerPosition.X - (ScreenWidth / 2);
            //position.Y = playerPosition.Y - (ScreenHeight / 0.5f);

            //ここの値をカメラの限界値？をいじれます。
            if (position.X < -5000)
                position.X = -5000;
            if (position.Y < -0)
                position.Y = -0;

            //scaleはズームの量
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                scale += 0.01f;
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                scale -= 0.01f;


            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0)) *
                Matrix.CreateScale(scale);

        }
    }
}
