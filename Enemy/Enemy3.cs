using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Zombie;

namespace teamwork1.Enemy
{
    class Enemy3:Enemy
    {
        private AI ai;
        public float PositionX;
        public float PositionY;

        public Enemy3(AI ai,string name, int hp, Vector2 position, Vector2 size, Vector2 velocity)
            : base("enemy3", hp, position, size, velocity)
        {
            this.ai = ai;
            PositionX = position.X;
            PositionY = position.Y;

        }
        public override void Initialize()
        {
            position = new Vector2(PositionX,PositionY);
        }

        public override void Update(GameTime gameTime)
        {
            position = ai.Think(this);
            
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
