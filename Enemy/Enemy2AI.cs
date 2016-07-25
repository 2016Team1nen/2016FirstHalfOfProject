using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie;
using Microsoft.Xna.Framework;
using Zombie.Device;
using Zombie.Active;

namespace teamwork1.Enemy
{
    class Enemy2AI:AI2
    {
        private Character other;

        private IsCollision isCollision;
        private Block block;
        private Vector2 size;
        private Vector2 Velocity;

        public Enemy2AI(Character other)
        {

            Velocity = new Vector2(-2, 0);


            this.other = other;
            isCollision = new IsCollision();
            block = new Block("block1", Vector2.Zero, new Vector2(192, 64));
            size = new Vector2(64, 64);
        }
        public override Vector2 Thinks(Enemy enemy)
        {
            enemy.SetPosition(ref position);


            Vector2 otherPosition = Vector2.Zero;
            other.SetPosition(ref otherPosition);

            Vector2 velocity = otherPosition - position;

            if (velocity == Vector2.Zero) { return position; }

            
            if (velocity.Length() >300)
            {
                velocity.Normalize();

                position += Velocity;
                foreach (var x in block.Screen1())
                {

                    bool enemyIsBlock = isCollision.Update(position, x.GetPosition(), size, x.GetSize());
                    if (enemyIsBlock)
                    {
                        Velocity = -Velocity;
                    }

                }

            }
            else
            {
                velocity.Normalize();
               

                position = new Vector2(position.X + velocity.X * 3f, position.Y+ 0.01f );

                foreach (var x in block.Screen1())
                {
                    bool enemyIsBlock = isCollision.Update(position, x.GetPosition(), size, x.GetSize());
                    if (enemyIsBlock)
                    {
                        position.Y -= 64;
                        if (enemyIsBlock)
                        {
                            if (enemyIsBlock && position.X < x.GetPosition().X &&
               position.Y <= (x.GetPosition() + x.GetSize()).Y - 2 &&
               position.Y >= x.GetPosition().Y + 2)
                            {
                                position.X = (x.GetPosition() - size).X - 1;

                            }

                            //右から行く(○)
                            if (enemyIsBlock && (position + size).X > (x.GetPosition() + x.GetSize()).X &&
                                position.Y <= (x.GetPosition() + x.GetSize()).Y - 2 &&
                                position.Y >= x.GetPosition().Y + 2)
                            {
                                position.X = (x.GetPosition() + x.GetSize()).X;
                            }

                            //上から落ちる(○)
                            if (enemyIsBlock && position.X >= (x.GetPosition() - size).X &&
                                position.X <= (x.GetPosition() + x.GetSize()).X &&
                                position.Y < x.GetPosition().Y)
                            {
                                velocity.Y = 0;
                                position.Y = x.GetPosition().Y - size.Y + 1;
                            }

                            //下から行く
                            if (enemyIsBlock && position.X >= (x.GetPosition() - size).X &&
                                (position + size).X <= (x.GetPosition() + x.GetSize()).X &&
                                position.Y > x.GetPosition().Y)
                            {
                                velocity *= -1;
                            }

                        }
                        
                    }

                }
            }

           


            return position;
        }
    }
}
