using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zombie
{
    class Block
    {
        private Vector2 position;
        private Vector2 size;
        private string name;
        private List<Block> block;


        public Block(string name, Vector2 position, Vector2 size)
        {
            this.name = name;
            this.position = position;
            this.size = size;
            block = new List<Block>();
        }


        public void Draw(Renderer renderer)
        {
            renderer.DrawTextureW(name, position);
        }

        public Vector2 GetPosition() {
            return position;
        }

        public Vector2 GetSize()
        {
            return size;
        }

        public List<Block> Screen1() {
            Vector2 p = new Vector2(32 * 25, Screen.screenHeight - 32 * 11);
            while (p.X < Screen.screenWidth - 32 * 2)
            {
                block.Add(new Block("block", p, new Vector2(32, 32)));
                p.X += 32;
            }

            p = new Vector2(32 * 10, Screen.screenHeight - 32 * 2);
            while (p.X < 1500)
            {
                block.Add(new Block("block", p, new Vector2(32, 32)));
                p.X += 32;
            }

            p = new Vector2(0, Screen.screenHeight - 32);
            while (p.X < 6000)
            {
                block.Add(new Block("block", p, new Vector2(32, 32)));
                p.X += 32;
            }

            return block;
        }




    }
}
