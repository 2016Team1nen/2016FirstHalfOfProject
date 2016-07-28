using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Zombie.Device;
using Zombie.Utility;

namespace Zombie.Active
{
    class MonsterA : Character
    {
        private bool isPursue;
        private Motion motion;
        
        public enum Direction { RIGHT, LEFT,STAND }
        private Direction direction;

        public MonsterA(string name, int hp, Vector2 position, Vector2 size, Vector2 velocity)
            : base(name, hp, position, size, velocity) {
            isPursue = false;
            Initialize();
        }

        public void Initialize() {
            motion = new Motion();

            //RUN
            for (int i = 0; i < 6; i++) {
                motion.Add(i, new Rectangle(300 * (i % 3), 260 * (int)(i / 3), 300, 260));
            }
            motion.Add(6, new Rectangle(300 * 0, 260 * 2, 300, 260));
            motion.Add(7, new Rectangle(300 * 1, 260 * 2, 300, 260));
            motion.Initialize(new Range(0, 7), new Timer(0.2f));

            //最初は右向き
            direction = Direction.RIGHT;
        }

        //動き
        public void Move(Vector2 player) {
            Falling();
            Pursue(player);
            if (isPursue && hp > 0) { velocity.X = -5;  }
            position += velocity;
        }

        //追いかけスイッチ
        public void Pursue(Vector2 player) {
            float dictance = (float)Math.Sqrt((player.X - position.X) * (player.X - position.X)
                                        + (player.Y - position.Y) * (player.Y - position.Y));
            if (dictance <= 1200) { isPursue = true; }
        }

        public override void Update(GameTime gameTime) { 
            motion.Update(gameTime);
            Timer timer = new Timer(1.0f);
            if (velocity.X == 0.0f && hp > 0) { motion.Initialize(new Range(0, 0), timer); }

            else if ((hp <= 0) && direction != Direction.STAND)
            {
                direction = Direction.STAND;
                timer = new Timer(3.0f);
                motion.Initialize(new Range(6, 7), timer);
            }

            else if ((velocity.X < 0.0f) && direction != Direction.LEFT) {
                direction = Direction.LEFT;
                motion.Initialize(new Range(3, 5), timer);
            }

            else if ((velocity.X > 0.0f) && direction != Direction.RIGHT) {
                direction = Direction.RIGHT;
                motion.Initialize(new Range(0, 2), timer);
            }
        }


        public override void Draw(Renderer renderer) {
            renderer.DrawTexture(name, position, motion.DrawingRange(),alpha);
        }
    }
}
