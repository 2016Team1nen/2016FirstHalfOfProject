﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Zombie.Device;
using Zombie.Active;

namespace Zombie
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;

        private Character player;

        private Block block;

        private Renderer renderer;

        private IsCollision isCollision;

        private Dictionary<string, Texture2D> textures;

        private Dictionary<string, Character> enemy;
        private List<Beam> beamR;
        private List<Beam> beamL;
        private List<Block> blockG;

        private InputState input;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = Screen.screenHeight;
            graphics.PreferredBackBufferWidth = Screen.screenWidth;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            textures = new Dictionary<string, Texture2D>();

            isCollision = new IsCollision();

            player = new Player("player", 3, new Vector2(0, Screen.screenHeight - 64 * 2), new Vector2(64, 64), Vector2.Zero);
            enemy = new Dictionary<string, Character>(){
                {"A",new EnemyA("enemy", 1, new Vector2(700, Screen.screenHeight - 64 * 3), new Vector2(64, 64), new Vector2(-5, 0))}
            };

            input = new InputState();
            beamR = new List<Beam>();
            beamL = new List<Beam>();       

            block = new Block("block", Vector2.Zero, new Vector2(192, 64));
            blockG = block.Screen1();

            renderer = new Renderer(Content, GraphicsDevice);

            base.Window.Title = "Zombie";
            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            renderer.LoadTexture("player");
            renderer.LoadTexture("enemy");
            renderer.LoadTexture("life");
            renderer.LoadTexture("block");
            renderer.LoadTexture("beam");

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            renderer.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape)) { this.Exit(); }

            // TODO: Add your update logic here

            
            //playerの移動
            player.Update();
            
            //enemyの移動
            foreach (var e in enemy){
                ((EnemyA)e.Value).Move(player.GetPosition());
            }

            //beamの種類のチェック
            input.ChangeBeam(Keyboard.GetState());
            
            
            //playerの向きをチェック
            int rf;
            rf = input.CheckRF(Keyboard.GetState());

            //beamの移動
            foreach (var b in beamR)
            {
                b.Update(); ;
            }
            foreach (var b in beamL)
            {
                b.Update(); ;
            }

            //shooting
            ((Player)player).Shoot(beamR, beamL, rf, input);

            //Blockとのあたり判定
            foreach (var b in blockG) {
                bool playerIsBlock = isCollision.Update(player.GetPosition(), b.GetPosition(), player.GetSize(), b.GetSize());
                if (playerIsBlock)
                {
                    player.IsFloor(b.GetPosition(), b.GetSize(), playerIsBlock);
                }
                
                foreach (var e in enemy) {
                    bool enemyIsBlock = isCollision.Update(e.Value.GetPosition(), b.GetPosition(), e.Value.GetSize(), b.GetSize());
                    if (enemyIsBlock)
                    {
                        e.Value.IsFloor(b.GetPosition(), b.GetSize(), enemyIsBlock);
                    }
                }

                foreach (var bR in beamR) {
                    bool beamRIsBlock = isCollision.Update(bR.GetPosition(), b.GetPosition(), bR.GetSize(), b.GetSize());
                    if (beamRIsBlock)
                    {
                        beamR.Remove(bR);
                        break;
                    }
                }

                foreach (var bL in beamL)
                {
                    bool beamLIsBlock = isCollision.Update(bL.GetPosition(), b.GetPosition(), bL.GetSize(), b.GetSize());
                    if (beamLIsBlock)
                    {
                        beamL.Remove(bL);
                        break;
                    }
                }
            }

            //windowとのあたり判定
            player.ChangePosition(isCollision.Collision(player.GetPosition()));
            player.Update();

            //敵とのあたり判定
            foreach (var e in enemy)
            {
                bool isEnemy = isCollision.Update(player.GetPosition(), e.Value.GetPosition(), player.GetSize(), e.Value.GetSize());
                if (isEnemy)
                {
                    player.ChangeHp(player.GetHp() - 1);
                    e.Value.ChangeHp(e.Value.GetHp() - 1);

                    player.IsEnemy(e.Value.GetPosition());
                }

                foreach (var bL in beamL) {
                    bool isBeamL = isCollision.Update(bL.GetPosition(), e.Value.GetPosition(), bL.GetSize(), e.Value.GetSize());
                    if (isBeamL) {
                        e.Value.ChangeHp(e.Value.GetHp() - 1);
                        beamL.Remove(bL);
                        break;
                    }
                }
                foreach (var bR in beamR)
                {
                    bool isBeamR = isCollision.Update(bR.GetPosition(), e.Value.GetPosition(), bR.GetSize(), e.Value.GetSize());
                    if (isBeamR)
                    {
                        e.Value.ChangeHp(e.Value.GetHp() - 1);
                        beamR.Remove(bR);
                        break;
                    }
                }
                

                if (e.Value.IsDeath())
                {
                    enemy.Remove(e.Key);
                    break;
                }

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            renderer.Begin();

            foreach (var b in blockG)
            {
                b.Draw(renderer);
            }

            //playerの向きをチェック
            int rf;
            rf = input.CheckRF(Keyboard.GetState());
            player.Draw(renderer, rf);

            foreach (var e in enemy){
                e.Value.Draw(renderer);
            }

            
            foreach (var b in beamL){
                b.Draw(renderer, ((Beam)b).GetBeamType());
            }
            foreach (var b in beamR)
            {
                b.Draw(renderer, ((Beam)b).GetBeamType());
            }

            foreach (var e in enemy)
            {
                renderer.DrawNumber(e.Value, e.Value.GetHp());
            }

            renderer.DrawNumber(player, player.GetHp());

            renderer.End();

            base.Draw(gameTime);
        }
    }
}
