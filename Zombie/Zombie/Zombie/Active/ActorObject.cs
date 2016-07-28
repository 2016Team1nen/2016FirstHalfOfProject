using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zombie
{
    abstract class ActorObject
    {
        protected string name;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 size;
        protected float alpha;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Vector2 Size
        {
            get { return size; }
        }

        public ActorObject(string name, Vector2 position, Vector2 velocity, Vector2 size)
        {
            this.name = name;
            this.position = position;
            this.velocity = velocity;
            this.size = size;
            alpha = 1.0f;
        }

        public abstract void Update(GameTime gameTime);

        //Draw
        public virtual void Draw(Renderer renderer) { renderer.DrawTextureW(name, position, alpha); }

        // Get
        public Vector2 GetPosition() { return position; }
        public Vector2 GetSize() { return size; }
        public Vector2 GetVelocity() { return velocity; }

        // Set
        public void ChangePosition(Vector2 position) { this.position = position; }
        public void ChangeVelocity(Vector2 velocity) { this.velocity = velocity; }
    }
}
