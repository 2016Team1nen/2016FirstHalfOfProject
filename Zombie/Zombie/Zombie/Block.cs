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


        public Block(string name, Vector2 position) {
            this.name = name;
            this.position = position;
            size = position + new Vector2(192, 64);
            block = new List<Block>();
        }


        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }

        public Vector2 GetPosition() {
            return position;
        }






        public List<Block> Screen1() {
            Vector2 p = new Vector2(192 * 3, Screen.screenHeight - 64 * 5);
            block.Add(new Block("block", p));

            return block;
        }


    }
}
