using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Zombie.Device;

namespace Zombie.Sceen
{
    interface ISceen
    {
        void Initialize();
        void Update(GameTime gameTime);
        void Draw(Renderer renderer);
        bool IsEnd();
        IsSceen Next();
    }
}
