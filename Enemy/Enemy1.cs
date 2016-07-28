using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Zombie;
using Zombie.Device;

namespace teamwork1.Enemy
{
    class Enemy1:Enemy
    {
        private float PositionX;
        private float PositionY;
        private IsCollision isCollision;
        private Block block;
        
        public Enemy1(string name, int hp, Vector2 position, Vector2 size, Vector2 velocity)
            : base(name, hp, position, size, velocity)
        {
            
            PositionX = position.X;
            PositionY = position.Y;
            isCollision = new IsCollision();
            block = new Block("block1", Vector2.Zero, new Vector2(192, 64));
        }

        public override void Initialize()
        {
            Velocity = new Vector2(-2, 0);
            position = new Vector2(PositionX, PositionY);

        }

        public override void Update(GameTime gameTime)
        {

            position = position + Velocity;

            foreach (var x in block.Screen1())
            {
                bool enemyIsBlock = isCollision.Update(position, x.GetPosition(), size, x.GetSize());
                if (enemyIsBlock)
                {
                    position.Y -= 64;
                    if (enemyIsBlock) { IsFloor(x.GetPosition(), x.GetSize(), enemyIsBlock); }
                    else { continue; }
                }

            }
            
            Falling();

            
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
