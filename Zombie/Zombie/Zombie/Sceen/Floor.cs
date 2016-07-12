using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zombie
{
    class Floor
    {
        private Vector2 position;
        private Vector2 size;
        private string name;
        private List<Floor> floor;


        public Floor(string name, Vector2 position, Vector2 size)
        {
            this.name = name;
            this.position = position;
            this.size = size;
            floor = new List<Floor>();
        }


        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetSize()
        {
            return size;
        }




        public List<Floor> Screen1()
        {
            Vector2 p = new Vector2();


            p = new Vector2(192 * 2, Screen.screenHeight - 64 * 2);
            floor.Add(new Floor("block", p, new Vector2(192, 64)));


            //while (p.X < Screen.screenWidth)
            //{
            //    floor.Add(new Floor("block", p, new Vector2(192, 64)));
            //    p.X += 192;
            //}


            p = new Vector2(0, Screen.screenHeight - 64);
            while (p.X < Screen.screenWidth)
            {
                floor.Add(new Floor("block", p, new Vector2(192, 64)));
                p.X += 192;
            }

            return floor;
        }


    }
}
