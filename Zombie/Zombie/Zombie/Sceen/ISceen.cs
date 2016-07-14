using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombie.Sceen
{
    interface ISceen
    {


        void Initialize();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, Dictionary<string, Texture2D> textures);
        bool IsEnd();
        Sceen Next();
    }
}
