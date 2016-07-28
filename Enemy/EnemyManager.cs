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
using teamwork1.Enemy;
using Zombie;
using Zombie.Device;

namespace teamwork1.Enemy
{
    class EnemyManager
    {
        private Character player;

        public List<Enemy> enemyList = new List<Enemy>();

        public EnemyManager(Character player)
        {
           
            this.player = player;
        }

        public void Initialize()
        {

            for (int i = 0; i < 1; i++)
            {
                enemyList.Add(new Enemy1("enemy", 0, Vector2.Zero, new Vector2(64, 64), new Vector2(-5, 0)));
            }

            for (int i = 0; i < 1; i++)
            {
                enemyList.Add(new Enemy2(new Enemy2AI(player), "enemy", 0, Vector2.Zero, new Vector2(64, 64), new Vector2(-5, 0)));
            }

           

            enemyList[0] = new Enemy1("enemy", 0,new Vector2(1344, Screen.screenHeight - 64 * 4), new Vector2(64, 64),Vector2.Zero);
            enemyList[0].Initialize();

            enemyList[1] = new Enemy2(new Enemy2AI(player), "enemy", 0, new Vector2(192*3+128, Screen.screenHeight - 64 * 2), new Vector2(64, 64), Vector2.Zero);
            enemyList[1].Initialize();

            //enemyList[0] = new Enemy3(new Enemy3AI(player, new Vector2(400, 444)), "enemy", 0, Vector2.Zero, new Vector2(400, 444), new Vector2(64, 64));
            //enemyList[0].Initialize();

        }

        public void Update(GameTime gameTime)
        {
            

            foreach (var enemy in enemyList) {
                enemy.Update(gameTime);
            }
            enemyList[0].Update(gameTime);
            enemyList[1].Update(gameTime);
        }

        public void Draw(Renderer renderer)
        {
           
            foreach (var enemy in enemyList) {
                //spriteBatch.Draw(textures["Enemy1"], enemy.position, Color.White);
                //spriteBatch.Draw(textures["Enemy2"], enemy.position, Color.White);
                //spriteBatch.Draw(textures["Enemy3"], enemy.position, Color.White);
                renderer.DrawTexture("enemy",enemy.GetPosition() );
               
            }
        }
    }
}
