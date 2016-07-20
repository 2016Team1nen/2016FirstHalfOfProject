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
        
        public Character(string name, int hp, Vector2 position, Vector2 size, Vector2 velocity)
        {
            this.name = name;
            this.hp = hp;
            this.position = position;
            this.velocity = velocity;
            this.size = size;
        }

        public abstract void Update(GameTime gameTime);
        protected void Falling() { velocity.Y += 1.0f; }

        public void IsBlock(Vector2 blockPosition, Vector2 bSize, bool isBlock)
        {
            //左から行く(○)
            if (isBlock && position.X < blockPosition.X &&
                position.Y <= (blockPosition + bSize).Y - 2 &&
                position.Y >= blockPosition.Y + 2) {
                position.X = (blockPosition - size).X - 1;
            }

            //右から行く(○)
            if (isBlock && (position + size).X > (blockPosition + bSize).X &&
                position.Y <= (blockPosition + bSize).Y - 2 &&
                position.Y >= blockPosition.Y + 2) {
                position.X = (blockPosition + bSize).X;
            }

            //上から落ちる(○)
            if (isBlock && position.X >= (blockPosition - size).X &&
                position.X <= (blockPosition + bSize).X &&
                position.Y < blockPosition.Y) {
                velocity.Y = 0;
                position.Y = blockPosition.Y - size.Y + 2;
            }

            //下から行く
            if (isBlock && position.X >= (blockPosition - size).X &&
                (position + size).X <= (blockPosition + bSize).X &&
                position.Y > blockPosition.Y)
            { velocity *= -1; }

        }

        public void IsEnemy(Vector2 enemyPosition) {
            if (position.X > enemyPosition.X) { position.X += 150; }
            else { position.X -= 150; }
        }


        //死亡判断
        public bool IsDeath() { return !(hp > 0); }


        ////////////////////////////////////////////
        

        //Get
        public string GetName() { return name; }
        public Vector2 GetPosition() { return position; }
        public Vector2 GetSize() { return size; }
        public int GetHp() { return hp; }
        public Vector2 GetVelocity() { return velocity; }

        
        //////////////////////////////////////////////////////
        

        //Change
        public void ChangeHp(int hp) { this.hp = hp; }
        public void ChangePosition(Vector2 position) { this.position = position; }
        public void ChangeVelocity(Vector2 velocity) { this.velocity = velocity; }


        ////////////////////////////////////////////


        //Draw
        public void Draw(Renderer renderer) { renderer.DrawTexture(name, position); }
        public void Draw(Renderer renderer, int rf) { renderer.Draw(name, position, rf); }


        //RefPosition、実装していない
        public void SetPosition(ref Vector2 other) { other = position; }

    }
}
