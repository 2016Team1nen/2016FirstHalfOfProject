using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Zombie.Device
{
    class DeviceManager
    {
        private InputState inputState;
        private Renderer renderer;
        private IsCollision isCollision;
        private Sound sound;
        private Select select;
        private Camera camera;
        private static Random rnd = new Random();

        public DeviceManager(ContentManager content,GraphicsDevice graphics) {
            inputState = new InputState();
            renderer = new Renderer(content, graphics);
            isCollision = new IsCollision();
            sound = new Sound(content);
            select = new Select();
            camera = new Camera();
        }

        public void Update(GameTime gameTime) { }

        public Renderer GetRenderer() { return renderer; }
        public InputState GetInputState() { return inputState; }
        public Sound GetSound() { return sound; }
        public IsCollision GetIsCollision() { return isCollision; }
        public Random GetRandom() { return rnd; }
        public Select GetSelect() { return select; }
        public Camera GetCamera() { return camera; }
    }
}
