using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Zombie
{
    class Renderer
    {
        private ContentManager contentManager;
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;

        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public Renderer(ContentManager content, GraphicsDevice graphics)
        {
            contentManager = content;
            graphicsDevice = graphics;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void LoadTexture(string name, string filepath = "./")
        {
            textures.Add(name, contentManager.Load<Texture2D>(filepath + name));
        }

        public void Unload()
        {
            textures.Clear();
        }

        public void Begin()
        {
            spriteBatch.Begin();
        }

        public void End()
        {
            spriteBatch.End();
        }

        public void DrawTexture(string name, Vector2 position, float alpha = 1.0f)
        {
            spriteBatch.Draw(textures[name], position, Color.White * alpha);
        }

        public void DrawNumber(Character chara, int life, float alpha = 1.0f)
        {

            Vector2 p = chara.GetPosition() + new Vector2(0, -10);
            for (int i = 0; i < chara.GetHp(); i++)
            {
                spriteBatch.Draw(textures["life"], p, Color.White * alpha);
                p.X += 20;
            }
        }


    }
}
