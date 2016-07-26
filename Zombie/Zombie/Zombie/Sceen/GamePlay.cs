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
        private List<Character> monster;

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
                new EnemyA("enemyA", 1, new Vector2(2200, Screen.screenHeight - 64 * 2-250), new Vector2(140, 250), new Vector2(-5, 0)),
                new EnemyB("enemyB", 1, new Vector2(4000, Screen.screenHeight - 64 * 2-250), new Vector2(140, 250),  Vector2.Zero)
            };
            monster = new List<Character>(){
                new MonsterA("monsterA", 1, new Vector2(3200, Screen.screenHeight - 64 * 2-260), new Vector2(300, 260), Vector2.Zero),
                new MonsterB("monsterB", 1, new Vector2(5000, Screen.screenHeight - 64 * 2-200), new Vector2(280, 200),  Vector2.Zero)
            };


            beamR = new List<Beam>();
            beamL = new List<Beam>();

            block = new Block("block", Vector2.Zero, new Vector2(64, 64));
            blockG = block.Screen1();

        }
        public void Update(GameTime gameTime)
        {
            sound.PlayeBGM("gameplaybgm");
            camera.Update(player.Position);

            //NextSceen判定
            if (player.Position.X >= 9000 - 1050 || player.Hp <= 0) { isEnd = true; sound.StopBGM(); }

            string s;
            

            //beamの移動
            foreach (var b in beamR) { b.Update(gameTime); }
            foreach (var b in beamL) { b.Update(gameTime); }

            //playerの動き
            player.Update(gameTime);

            //ListGet
            beamL = ((Player)player).GetBeamL();
            beamR = ((Player)player).GetBeamR();

            //enemyの動き
            foreach (var e in enemy)
            {
                if (e is EnemyA) { ((EnemyA)e).Move(player.Position); }
                else { ((EnemyB)e).Move(player.Position); }
                e.Update(gameTime);
            }

            //monsterの動き
            foreach (var m in monster)
            {
                if (m is MonsterA) { ((MonsterA)m).Move(player.Position); }
                else { ((MonsterB)m).Move(player.Position); }
                m.Update(gameTime);
            }

            //Blockとのあたり判定
            foreach (var b in blockG)
            {
                //Playerとのあたり判定
                bool playerIsBlock = isCollision.Update(player.Position, b.GetPosition(), player.Size, b.GetSize());
                if (playerIsBlock) { player.IsBlock(b.GetPosition(), b.GetSize(), playerIsBlock); }

                //Enemyとのあたり判定
                foreach (var e in enemy)
                {
                    bool enemyIsBlock = isCollision.Update(e.Position, b.GetPosition(), e.Size, b.GetSize());
                    if (enemyIsBlock) { e.IsBlock(b.GetPosition(), b.GetSize(), enemyIsBlock); }
                }

                //Monsterとのあたり判定
                foreach (var m in monster)
                {
                    bool monsterIsBlock = isCollision.Update(m.Position, b.GetPosition(), m.Size, b.GetSize());
                    if (monsterIsBlock) { m.IsBlock(b.GetPosition(), b.GetSize(), monsterIsBlock); }
                }

                //弾とのあたり判定
                foreach (var bR in beamR)
                {
                    bool beamRIsBlock = isCollision.Update(bR.Position, b.GetPosition(), bR.Size, b.GetSize());
                    if (beamRIsBlock) { beamR.Remove(bR); break; }
                }
                foreach (var bL in beamL)
                {
                    bool beamLIsBlock = isCollision.Update(bL.Position, b.GetPosition(), bL.Size, b.GetSize());
                    if (beamLIsBlock) { beamL.Remove(bL); break; }
                }
            }

            //windowとのあたり判定
            player.Position = isCollision.Collision(player.Position);

            //敵のあたり判定
            foreach (var e in enemy)
            {
                if (e.Hp <= 0) { e.IsDeath(); continue; }
                //敵とプレーヤーのあたり判定
                bool isEnemy = isCollision.Update(player.Position, e.Position, player.Size, e.Size);
                if (isEnemy)
                {
                    sound.PlaySE("gameplayse");
                    player.Hp = player.Hp - 1;
                    e.Hp = e.Hp - 1;
                    player.IsEnemy(e.Position);
                }

                //敵と弾のあたり判定
                foreach (var bL in beamL)
                {
                    if (!bL.GetBeamType()) { continue; }
                    bool isBeamL = isCollision.Update(bL.Position, e.Position, bL.Size, e.Size);
                    if (isBeamL)
                    {
                        sound.PlaySE("gameplayse");
                        e.Hp = e.Hp - 1;
                        beamL.Remove(bL);
                        break;
                    }
                }
                foreach (var bR in beamR)
                {
                    if (!bR.GetBeamType()) { continue; }
                    bool isBeamR = isCollision.Update(bR.Position, e.Position, bR.Size, e.Size);
                    if (isBeamR)
                    {
                        sound.PlaySE("gameplayse");
                        e.Hp = e.Hp - 1;
                        beamR.Remove(bR);
                        break;
                    }
                }
            }
            enemy.RemoveAll(e => e.Alpha <= 0);


            //敵のあたり判定
            foreach (var m in monster)
            {
                if (m.Hp <= 0) { m.IsDeath(); continue; }
                //敵とプレーヤーのあたり判定
                bool isEnemy = isCollision.Update(player.Position, m.Position, player.Size, m.Size);
                if (isEnemy){
                    sound.PlaySE("gameplayse");
                    player.Hp = player.Hp - 1;
                    m.Hp = m.Hp - 1;
                    player.IsEnemy(m.Position);
                }

                //敵と弾のあたり判定
                foreach (var bL in beamL)
                {
                    if (bL.GetBeamType()) { continue; }
                    bool isBeamL = isCollision.Update(bL.Position, m.Position, bL.Size, m.Size);
                    if (isBeamL)
                    {
                        sound.PlaySE("gameplayse");
                        m.Hp = m.Hp - 1;
                        beamL.Remove(bL);
                        break;
                    }
                }
                foreach (var bR in beamR)
                {
                    if (bR.GetBeamType()) { continue; }
                    bool isBeamR = isCollision.Update(bR.Position, m.Position, bR.Size, m.Size);
                    if (isBeamR)
                    {
                        sound.PlaySE("gameplayse");
                        m.Hp = m.Hp - 1;
                        beamR.Remove(bR);
                        break;
                    }
                }
            }
            monster.RemoveAll(m => m.Alpha <= 0);
        }

        public void Draw(Renderer renderer) {
            renderer.DrawCamera(camera);

            renderer.DrawTextureW("sky", Vector2.Zero);
            renderer.DrawTextureW("sky", new Vector2(3000, 0));
            renderer.DrawTextureW("sky", new Vector2(6000, 0));
            renderer.DrawTextureW("start", new Vector2(400, 330));
            renderer.DrawTextureW("goal", new Vector2(5320, 0));


            //敵の表示
            foreach (var e in enemy) { e.Draw(renderer); }
            foreach (var m in monster) { m.Draw(renderer); }

            //playerの表示
            player.Draw(renderer);

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
            if (player.Hp<=0){return IsSceen.ENDING;}
            else{return IsSceen.CLEAR;}
        }

    }
}
