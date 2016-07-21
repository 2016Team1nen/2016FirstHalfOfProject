using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Zombie.Device;
using Zombie.Active;

namespace Zombie.Sceen
{
    class GamePlay : ISceen
    {
        private Character player;
        private List<Character> enemy;

        private Block block;
        private List<Block> blockG;

        private List<Beam> beamR;
        private List<Beam> beamL;

        private IsCollision isCollision;
        private Sound sound;
        private bool isEnd;

        public GamePlay(DeviceManager deviceManager) {
            isCollision = deviceManager.GetIsCollision();
            sound = deviceManager.GetSound();
        }

        public void Initialize() {
            isEnd = false;

            player = new Player("player", 3, new Vector2(0, Screen.screenHeight - 64 * 2), new Vector2(64, 64), Vector2.Zero);
            enemy = new List<Character>(){
                new EnemyA("enemy", 1, new Vector2(700, Screen.screenHeight - 64 * 3), new Vector2(64, 64), new Vector2(-5, 0))
            };

            beamR = new List<Beam>();
            beamL = new List<Beam>();

            block = new Block("block", Vector2.Zero, new Vector2(192, 64));
            blockG = block.Screen1();

        }
        public void Update(GameTime gameTime) {
            sound.PlayeBGM("gameplaybgm");

            //Ending判定
            if (player.GetPosition().X >= Screen.screenWidth - 64) { isEnd = true; sound.StopBGM(); }

            //beamの移動
            foreach (var b in beamR) { b.Update(gameTime);  }
            foreach (var b in beamL) { b.Update(gameTime);  }

            //playerの動き
            player.Update(gameTime);

            //ListGet
            beamL = ((Player)player).GetBeamL();
            beamR = ((Player)player).GetBeamR();

            //enemyの移動
            foreach (var e in enemy) { ((EnemyA)e).Move(player.GetPosition()); }

            //Blockとのあたり判定
            foreach (var b in blockG)
            {
                //Playerとのあたり判定
                bool playerIsBlock = isCollision.Update(player.GetPosition(), b.GetPosition(), player.GetSize(), b.GetSize());
                if (playerIsBlock) { player.IsBlock(b.GetPosition(), b.GetSize(), playerIsBlock); }

                //Enemyとのあたり判定
                foreach (var e in enemy) {
                    bool enemyIsBlock = isCollision.Update(e.GetPosition(), b.GetPosition(), e.GetSize(), b.GetSize());
                    if (enemyIsBlock) { e.IsBlock(b.GetPosition(), b.GetSize(), enemyIsBlock); }
                }
                
                //弾とのあたり判定
                foreach (var bR in beamR) {
                    bool beamRIsBlock = isCollision.Update(bR.GetPosition(), b.GetPosition(), bR.GetSize(), b.GetSize());
                    if (beamRIsBlock) { beamR.Remove(bR);   break; }
                }
                foreach (var bL in beamL) {
                    bool beamLIsBlock = isCollision.Update(bL.GetPosition(), b.GetPosition(), bL.GetSize(), b.GetSize());
                    if (beamLIsBlock) { beamL.Remove(bL);   break; }
                }
            }

            //windowとのあたり判定
            player.ChangePosition(isCollision.Collision(player.GetPosition()));

            //敵のあたり判定
            foreach (var e in enemy) {
                //敵とプレーヤーのあたり判定
                bool isEnemy = isCollision.Update(player.GetPosition(), e.GetPosition(), player.GetSize(), e.GetSize());
                if (isEnemy) {
                    sound.PlaySE("gameplayse");
                    player.ChangeHp(player.GetHp() - 1);
                    e.ChangeHp(e.GetHp() - 1);
                    player.IsEnemy(e.GetPosition());
                }

                //敵と弾のあたり判定
                foreach (var bL in beamL) {
                    bool isBeamL = isCollision.Update(bL.GetPosition(), e.GetPosition(), bL.GetSize(), e.GetSize());
                    if (isBeamL) {
                        sound.PlaySE("gameplayse");
                        e.ChangeHp(e.GetHp() - 1);
                        beamL.Remove(bL);
                        break;
                    }
                }
                foreach (var bR in beamR) {
                    bool isBeamR = isCollision.Update(bR.GetPosition(), e.GetPosition(), bR.GetSize(), e.GetSize());
                    if (isBeamR) {
                        sound.PlaySE("gameplayse");
                        e.ChangeHp(e.GetHp() - 1);
                        beamR.Remove(bR);
                        break;
                    }
                }
            }
            enemy.RemoveAll(e => e.IsDeath());


        }


        public void Draw(Renderer renderer) {
            renderer.Begin();

            //playerの向きをチェック
            int rf;
            rf = ((Player)player).GetRL();
            player.Draw(renderer, rf);

            //敵の表示
            foreach (var e in enemy) { e.Draw(renderer); }

            //ステージの表示
            foreach (var b in blockG) { b.Draw(renderer); }

            //弾の表示
            foreach (var b in beamL) { b.Draw(renderer, ((Beam)b).GetBeamType()); }
            foreach (var b in beamR) { b.Draw(renderer, ((Beam)b).GetBeamType()); }

            //HPの表示
            foreach (var e in enemy) { renderer.DrawNumber(e, e.GetHp()); }
            renderer.DrawNumber(player, player.GetHp());

            renderer.End();
        }



        public bool IsEnd() {
            return isEnd;
        }
        public IsSceen Next() {
            Initialize();
            return IsSceen.ENDING;
        }

    }
}
