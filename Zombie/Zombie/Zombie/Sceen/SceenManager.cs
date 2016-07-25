using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zombie.Device;

namespace Zombie.Sceen
{
    class SceenManager
    {
        private Dictionary<IsSceen, ISceen> sceens = new Dictionary<IsSceen,ISceen>();
        private ISceen currentSceen = null;

        public SceenManager() { }

        public void Add(IsSceen name, ISceen sceen)
        {
            if(sceens.ContainsKey(name)){
                return;
            }
            sceens.Add(name, sceen);
        }

        public void Change(IsSceen name)
        {
            if (currentSceen != null) { 
            }
            currentSceen = sceens[name];
            currentSceen.Initialize();
        }

        public void Update(GameTime gameTime){
            if (currentSceen == null) {
                return;
            }
            currentSceen.Update(gameTime);

            if (currentSceen.IsEnd()) {
                Change(currentSceen.Next());
            }
        }

        public void Draw(Renderer renderer)
        {
            if (currentSceen == null)
            {
                return;
            }
            currentSceen.Draw(renderer);
        }
        public void cameraDraw(Camera camera,Player player)
        {
            cameraDraw(camera,player);
        }
    }
}
