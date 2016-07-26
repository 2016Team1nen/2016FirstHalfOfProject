using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Zombie.Device;
using Zombie.Utility;

namespace Zombie
{
    class EnemyA : Character
    {
        private bool isPursue;
        private Vector2 roop;
        private Motion motion;

        public enum Direction { RIGHT, LEFT }
        private Direction direction;

        public EnemyA(string name, int hp, Vector2 position, Vector2 size, Vector2 velocity)
            : base(name, hp, position, size, velocity) {
                isPursue = false;
                roop = position;
                Initialize();
        }


        public void Initialize() {
            motion = new Motion();

            //RUN
            for (int i = 0; i < 6; i++){
                motion.Add(i, new Rectangle(140 * (i % 3), 250 * (int)(i / 3), 140, 250));
            }
            motion.Initialize(new Range(0, 5), new Timer(0.2f));

            //最初は左向き
            direction = Direction.LEFT;
        }

        //動き
        public void Move(Vector2 player) {
            Falling();
            Pursue(player);
            if (isPursue) {
                if (player.X - position.X > 0 && velocity.X < 0)  { velocity.X *= -1; }
                else if (player.X - position.X < 0 && velocity.X > 0) {  velocity.X *= -1; }
            }
            else {
                if (position.X < roop.X - 300) { velocity.X *= -1; }
                if (position.X > roop.X + 300) { velocity.X *= -1; }
            }
            position += velocity;
        }

        //追いかけスイッチ
        public void Pursue(Vector2 player) {
            float dictance = (float)Math.Sqrt((player.X - position.X) * (player.X - position.X) 
                                        + (player.Y - position.Y) * (player.Y - position.Y));
            if (dictance <= 600) { isPursue = true; }
        }

        public override void Update(GameTime gameTime) {
            motion.Update(gameTime);
            Timer timer = new Timer(1.0f);
            if ((velocity.X < 0.0f) && direction != Direction.LEFT) {
                direction = Direction.LEFT;
                motion.Initialize(new Range(3, 5), timer);
            }

            else if ((velocity.X > 0.0f) && direction != Direction.RIGHT) {
                direction = Direction.RIGHT;
                motion.Initialize(new Range(0, 2), timer);
            }
        }

        public override void Draw(Renderer renderer) {
            renderer.DrawTexture(name, position, motion.DrawingRange());
        }

    }
}
