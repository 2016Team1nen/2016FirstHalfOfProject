using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


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

        public abstract void Move();
        public abstract void Update();

        public Vector2 Collision()
        {
            position = Vector2.Clamp(position, Vector2.Zero, new Vector2(Screen.screenWidth - 64, Screen.screenHeight - 64));
            return position;
        }

        public void Draw(Renderer renderer) {
            renderer.DrawTexture(name, position);
        }


        public bool IsDeath()
        {
            if (hp > 0) { return false; }
            else { return true; }
        }




        public bool IsCollision(Vector2 other)
        {
            if (//左下から行く
                size.X >= other.X && size.X <= (other.X + 64) &&
                position.Y >= other.Y && position.Y <= (other.Y + 64))
            {
                position.X -= 150;
                return true;
            }
            if (//右上から行く
                position.X >= other.X && position.X <= (other.X + 64) &&
                size.Y >= other.Y && size.Y <= (other.Y + 64))
            {
                position.X += 150;
                return true;
            }

            else
            {
                return false;
            }
        }

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

    }
}
