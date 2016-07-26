using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Zombie.Active;
using Zombie.Device;
using Zombie.Utility;

namespace Zombie
{
    class Player : Character
    {
        private InputState input;
        private Motion motion;
        
        private int rl;
        private bool beamType;
        private List<Beam> beamR;
        private List<Beam> beamL;

        public enum Direction { RIGHT, LEFT, JUMP, STOP }

        private Direction direction;


        public Player(string name, int hp, Vector2 position, Vector2 size, Vector2 velocity)
            : base(name, hp, position, size, velocity) {
                input = new InputState();
                beamL = new List<Beam>();
                beamR = new List<Beam>();
                rl = 1;
                beamType = true;
                Initialize();
        }


        public void Initialize() {
            motion = new Motion();

            //RUN
            for (int i = 0; i < 6; i++) {
                motion.Add(i, new Rectangle(211 * (i % 3), 250 * (int)(i / 3), 211, 250));
            }

            //JUMP
            motion.Add(6, new Rectangle(211 * 0, 250 * 2, 211, 250));
            motion.Add(7, new Rectangle(211 * 1, 250 * 2, 211, 250));
            
            //STOP
            motion.Add(8, new Rectangle(211 * 0, 250 * 3, 211, 250));
            motion.Add(9, new Rectangle(211 * 1, 250 * 3, 211, 250));

            motion.Initialize(new Range(0, 9), new Timer(0.2f));

            //最初は右向き
            direction = Direction.RIGHT;
        }


        public override void Update(GameTime gameTime) {
            input.UpdateKey(Keyboard.GetState());
            Jump();
            Falling();
            Move();
            ChangeBeam();
            Shoot();

            motion.Update(gameTime);

            Timer timer = new Timer(1.0f);
            if ((velocity.Y < -10.0f) && (velocity.Y > 10.0f) && direction != Direction.JUMP) {
                direction = Direction.JUMP;
                if (rl >= 0) { motion.Initialize(new Range(6, 6), timer); }
                else { motion.Initialize(new Range(7, 7), timer); }
            }

            else if ((velocity.X == 0.0f) && direction != Direction.STOP) {
                direction = Direction.STOP;
                if (rl > 0) { motion.Initialize(new Range(8, 8), timer); }
                else { motion.Initialize(new Range(9, 9), timer); }
            }


            else if ( (velocity.X < 0.0f) && direction != Direction.LEFT) {
                direction = Direction.LEFT;
                motion.Initialize(new Range(0, 2), timer);
            }

            else if ( (velocity.X > 0.0f) && direction != Direction.RIGHT) {
                direction = Direction.RIGHT;
                motion.Initialize(new Range(3, 5), timer);
            }
        }


        private void Shoot() { 
            if (input.IsKeyDown(Keys.Space)) {
                if (rl > 0) {
                    beamR.Add(new Beam("beam", 1, (position + new Vector2(size.X,50)), new Vector2(32, 32), Vector2.Zero, beamType, rl));
                }
                else {
                    beamL.Add(new Beam("beam", 1, (position + new Vector2(-16, 50)), new Vector2(32, 32), Vector2.Zero, beamType, rl));
                }
            }
            beamR.RemoveAll(b => b.GetStart() < (b.Position.X - 500));
            beamL.RemoveAll(b => b.GetStart() > (b.Position.X + 500));
        }
        
        //beamの種類を切り替える true:水色、false:ピンク
        private void ChangeBeam() {
            if (input.IsKeyDown(Keys.Q))
            { beamType = !beamType; }
        }

        //rl : right=1,left=-1
        private void Move() {
            float speed = 5;
            if (input.IsDown(Keys.Left)) { velocity.X = -1; rl = -1; }
            else if (input.IsDown(Keys.Right)) { velocity.X = 1; rl = 1; }
            else { velocity.X = 0;  }
            position.X += velocity.X * speed;
        }

        private void Jump() {
            if (velocity.Y == 0 && input.IsDown(Keys.Up)) {
                velocity.Y -= 25.6f;
            }
            position.Y += velocity.Y;
        }

        public int GetRL() { return rl; }
        public List<Beam> GetBeamL() { return beamL; }
        public List<Beam> GetBeamR() { return beamR; }

        public override void Draw(Renderer renderer) {
            renderer.DrawTexture(name, position, motion.DrawingRange(), alpha);
        }

    }
}
