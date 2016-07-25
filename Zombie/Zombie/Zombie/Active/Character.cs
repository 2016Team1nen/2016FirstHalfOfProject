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

        protected void Falling()  
        {
            velocity.Y += 0.5f;
            position.Y += velocity.Y;
        }


        public void IsFloor(Vector2 floorPosition, Vector2 fSize, bool isFloor) {
            //左から行く(○)
            if (isFloor && position.X < floorPosition.X &&
                position.Y <= (floorPosition + fSize).Y - 2 &&
                position.Y >= floorPosition.Y + 2)
            {
                position.X = (floorPosition - size).X - 1;
            }

            //右から行く(○)
            if (isFloor && (position + size).X > (floorPosition + fSize).X &&
                position.Y <= (floorPosition + fSize).Y - 2 &&
                position.Y >= floorPosition.Y + 2)
            {
                position.X = (floorPosition + fSize).X;
            }

            //上から落ちる(○)
            if (isFloor && position.X >= (floorPosition - size).X &&
                position.X <= (floorPosition + fSize).X &&
                position.Y < floorPosition.Y)
            {
                velocity.Y = 0;
                position.Y = floorPosition.Y - size.Y + 1;
            }

            //下から行く
            if (isFloor && position.X >= (floorPosition - size).X &&
                (position + size).X <= (floorPosition + fSize).X &&
                position.Y > floorPosition.Y)
            {
                velocity *= -1;
            }
        }


        public void IsEnemy(Vector2 enemyPosition)
        {
            if (position.X > enemyPosition.X) {
                position.X += 150;
            }

            else {
                position.X -= 150;
            }
        }

        public bool IsDeath()
        {
            if (hp > 0) { return false; }
            else { return true; }
        }


        ////////////////////////////////////////////
        

        //Get
        public string GetName()
        {
            return name;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetSize()
        {
            return size;
        }

        public int GetHp()
        {
            return hp;
        }

        public Vector2 GetVelocity() {
            return velocity;
        }

        
        //////////////////////////////////////////////////////
        

        //Change
        public void ChangeHp(int hp) {
            this.hp = hp;
        }

        public void ChangePosition(Vector2 position)
        {
            this.position = position;
        }

        public void ChangeVelocity(Vector2 velocity)
        {
            this.velocity = velocity;
        }


        ////////////////////////////////////////////


        //Draw
        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }

        public void Draw(Renderer renderer, int rf)
        {
            renderer.DrawTexture(name, position, rf);
        }

        //RefPosition
        public void SetPosition(ref Vector2 other)
        {
            other = position;
        }

        public abstract void Update();

    }
}
