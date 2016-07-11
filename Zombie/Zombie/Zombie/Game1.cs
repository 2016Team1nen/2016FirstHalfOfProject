using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Zombie
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;

        private Character player;
        private Floor floor;
        private Block block;

        private Renderer renderer;

        Dictionary<string, Texture2D> textures;

        Dictionary<string, Character> enemy;

        List<Floor> floorG;
        List<Block> blockG;

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

            player = new Player("player", 3, new Vector2(0, Screen.screenHeight - 64 * 2), Vector2.Zero, Vector2.Zero);
            enemy = new Dictionary<string, Character>(){
                {"A",new EnemyA("enemy", 1, new Vector2(700, Screen.screenHeight - 64 * 3), Vector2.Zero, new Vector2(-5, 0))}
            };
            
            floor = new Floor("block", Vector2.Zero);
            floorG = floor.Screen1();

            block = new Block("block", Vector2.Zero);
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

            //左右の移動
            //player.Move();

            foreach (var e in enemy)
            {
                ((EnemyA)e.Value).Move(player.GetPosition());
            }

            
            //windowとのあたり判定
            player.Collision();
            player.Update();

            //敵とのあたり判定
            foreach (var e in enemy)
            {
                if (((Player)player).IsCollision(e.Value.GetPosition()) == true)
                {
                    player.ChangeHp(player.GetHp() - 1);
                    e.Value.ChangeHp(e.Value.GetHp() - 1);
                    if (e.Value.IsDeath())
                    {
                        enemy.Remove(e.Key);
                        break;
                    }
                }
            }



            //rendermonkey

            foreach (var b in blockG)
            {
                ((Player)player).IsBlock(b.GetPosition());
            }




            

            foreach (var f in floorG)
            {
                if (player.GetPosition().X >= f.GetPosition().X - 64 && player.GetPosition().X < f.GetPosition().X + 128 &&
                    player.GetPosition().Y >= 0 && player.GetPosition().Y <= f.GetPosition().Y)
                {
                    ((Player)player).IsFloor(f.GetPosition());
                }
            }

            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            renderer.Begin();


            foreach (var f in floorG)
            {
                f.Draw(renderer);
            }

            foreach (var b in blockG)
            {
                b.Draw(renderer);
            }


            player.Draw(renderer);

            foreach (var e in enemy)
            {
                e.Value.Draw(renderer);
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
