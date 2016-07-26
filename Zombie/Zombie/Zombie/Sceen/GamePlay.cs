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
        private Camera camera;
        private bool isEnd;

        public GamePlay(DeviceManager deviceManager) {
            isCollision = deviceManager.GetIsCollision();
            sound = deviceManager.GetSound();
            camera = deviceManager.GetCamera();
        }

        public void Initialize() {
            isEnd = false;

            player = new Player("player", 3, new Vector2(500, Screen.screenHeight - 64 -250), new Vector2(211, 250), Vector2.Zero);
            enemy = new List<Character>(){
                new EnemyA("enemyA", 1, new Vector2(2200, Screen.screenHeight - 64 * 2-250), new Vector2(139, 250), new Vector2(-5, 0)),
                new EnemyB("enemyB", 1, new Vector2(4000, Screen.screenHeight - 64 * 2-250), new Vector2(139, 250),  Vector2.Zero)
            };

            beamR = new List<Beam>();
            beamL = new List<Beam>();

            block = new Block("block", Vector2.Zero, new Vector2(64, 64));
            blockG = block.Screen1();

        }
        public void Update(GameTime gameTime) {
            sound.PlayeBGM("gameplaybgm");
            camera.Update(player.Position);
            
            //Ending判定
            if (player.Position.X >= 6000 -1050) { isEnd = true; sound.StopBGM(); }

            //beamの移動
            foreach (var b in beamR) { b.Update(gameTime);  }
            foreach (var b in beamL) { b.Update(gameTime);  }

            //playerの動き
            player.Update(gameTime);

            //ListGet
            beamL = ((Player)player).GetBeamL();
            beamR = ((Player)player).GetBeamR();

            //enemyの動き
            foreach (var e in enemy) {
                if (e is EnemyA) { ((EnemyA)e).Move(player.Position); }
                else { ((EnemyB)e).Move(player.Position); }
                e.Update(gameTime);
            }

            //Blockとのあたり判定
            foreach (var b in blockG)
            {
                //Playerとのあたり判定
                bool playerIsBlock = isCollision.Update(player.Position, b.GetPosition(), player.Size, b.GetSize());
                if (playerIsBlock) { player.IsBlock(b.GetPosition(), b.GetSize(), playerIsBlock); }

                //Enemyとのあたり判定
                foreach (var e in enemy) {
                    bool enemyIsBlock = isCollision.Update(e.Position, b.GetPosition(), e.Size, b.GetSize());
                    if (enemyIsBlock) { e.IsBlock(b.GetPosition(), b.GetSize(), enemyIsBlock); }
                }
                
                //弾とのあたり判定
                foreach (var bR in beamR) {
                    bool beamRIsBlock = isCollision.Update(bR.Position, b.GetPosition(), bR.Size, b.GetSize());
                    if (beamRIsBlock) { beamR.Remove(bR);   break; }
                }
                foreach (var bL in beamL) {
                    bool beamLIsBlock = isCollision.Update(bL.Position, b.GetPosition(), bL.Size, b.GetSize());
                    if (beamLIsBlock) { beamL.Remove(bL);   break; }
                }
            }

            //windowとのあたり判定
            player.Position = isCollision.Collision(player.Position);

            //敵のあたり判定
            foreach (var e in enemy) {
                //敵とプレーヤーのあたり判定
                bool isEnemy = isCollision.Update(player.Position, e.Position, player.Size, e.Size);
                if (isEnemy) {
                    sound.PlaySE("gameplayse");
                    player.Hp = player.Hp - 1;
                    e.Hp = e.Hp - 1;
                    player.IsEnemy(e.Position);
                }

                //敵と弾のあたり判定
                foreach (var bL in beamL) {
                    bool isBeamL = isCollision.Update(bL.Position, e.Position, bL.Size, e.Size);
                    if (isBeamL) {
                        sound.PlaySE("gameplayse");
                        e.Hp = e.Hp - 1;
                        beamL.Remove(bL);
                        break;
                    }
                }
                foreach (var bR in beamR) {
                    bool isBeamR = isCollision.Update(bR.Position, e.Position, bR.Size, e.Size);
                    if (isBeamR) {
                        sound.PlaySE("gameplayse");
                        e.Hp = e.Hp - 1;
                        beamR.Remove(bR);
                        break;
                    }
                }
            }
            enemy.RemoveAll(e => e.IsDeath());
        }


        public void Draw(Renderer renderer) {
            renderer.DrawCamera(camera);
            renderer.DrawTextureW("sky", Vector2.Zero);
            renderer.DrawTextureW("sky", new Vector2(3000, 0));
            renderer.DrawTextureW("goal", new Vector2(3920, 0));

            //playerの表示
            player.Draw(renderer);

            //敵の表示
            foreach (var e in enemy) { e.Draw(renderer); }

            //ステージの表示
            foreach (var b in blockG) { b.Draw(renderer); }

            //弾の表示
            foreach (var b in beamL) { b.Draw(renderer, ((Beam)b).GetBeamType()); }
            foreach (var b in beamR) { b.Draw(renderer, ((Beam)b).GetBeamType()); }

            //HPの表示
            foreach (var e in enemy) { renderer.DrawNumber(e, e.Hp); }
            renderer.DrawNumber(player, player.Hp);

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
