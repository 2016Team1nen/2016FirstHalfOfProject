﻿using System;
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
            Vector2 p = new Vector2(192 * 3, Screen.screenHeight - 64 * 5);
            block.Add(new Block("block", p, new Vector2(192, 64)));

            p = new Vector2(192 * 2, Screen.screenHeight - 64 * 2);
            while (p.X < Screen.screenWidth - 192*2)
            {
                block.Add(new Block("block", p, new Vector2(192, 64)));
                p.X += 192;
            }

            p = new Vector2(0, Screen.screenHeight - 64);
            while (p.X < Screen.screenWidth)
            {
                block.Add(new Block("block", p, new Vector2(192, 64)));
                p.X += 192;
            }

            return block;
        }




    }
}
