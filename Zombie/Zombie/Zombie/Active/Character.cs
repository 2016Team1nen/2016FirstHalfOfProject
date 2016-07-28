using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zombie
{
    abstract class Character
    {
        protected string name;
        protected int hp;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 size;
        protected float alpha;

        protected bool isblock;

        public Character(string name, int hp, Vector2 position, Vector2 size, Vector2 velocity) {
            this.name = name;
            this.hp = hp;
            this.position = position;
            this.velocity = velocity;
            this.size = size;
            isblock = false;
            alpha = 1.0f;
        }

        public abstract void Update(GameTime gameTime);
        protected void Falling() { velocity.Y += 1.0f; }

        public void IsBlock(Vector2 blockPosition, Vector2 bSize, bool isBlock)
        {
            isblock = isBlock;

            //左から行く(○)
            if (isBlock && position.X <= (blockPosition.X - size.X + 5) &&
                position.Y < ((blockPosition + bSize).Y) &&
                position.Y > ((blockPosition - size).Y + 3) ) {
                velocity.Y = 1.0f;
                position.X = (blockPosition - size).X;
            }

            //右から行く(○)
            if (isBlock && (position.X + 5) >= (blockPosition + bSize).X && 
                position.Y < ((blockPosition + bSize).Y)&&
                position.Y > (blockPosition - size).Y + 3) {
                velocity.Y = 1.0f;
                position.X = (blockPosition + bSize).X;
            }

            //上から落ちる(○)
            if (isBlock && position.X > (blockPosition - size).X &&
                position.X < (blockPosition + bSize).X &&
                (position + size).Y -30  < blockPosition.Y) {
                velocity.Y = 0;
                position.Y = blockPosition.Y - size.Y;
            }

            //下から行く(○)
            if (isBlock && position.X >= (blockPosition - size).X -5 &&
                position.X <= (blockPosition + bSize).X+ 5 &&
                position.Y >= blockPosition.Y){
                velocity.Y *= -1; }
        }

        public void IsEnemy(Vector2 enemyPosition) {
            if (position.X > enemyPosition.X) { position.X += 150; }
            else { position.X -= 150; }
        }

        //死亡判断
        public void IsDeath() { 
            if (hp > 0) { return;}
            velocity = Vector2.Zero;
            alpha -= 0.005f;
        }

        ////////////////////////////////////////////

        //Get,Set
        public string GetName() { return name; }

        public int Hp { 
            get { return hp; }
            set { hp = value; }
        }

        public Vector2 Position { 
            get { return position; }
            set { position = value; }
        }
        public Vector2 Velocity { get { return velocity; } }
        public Vector2 Size { get { return size; } }
        public float Alpha { 
            get { return alpha; }
            set { alpha = value; }
        }

        ////////////////////////////////////////////

        //Draw
        public virtual void Draw(Renderer renderer) { renderer.DrawTextureW(name, position, alpha); }

        //RefPosition、実装していない
        public void SetPosition(ref Vector2 other) { other = position; }

    }
}
