using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Zombie;

namespace teamwork1.Enemy
{
    abstract class Enemy:Character
    {
        protected Vector2 Velocity;



        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="radius">半径</param>
        public Enemy(string name, int hp, Vector2 position, Vector2 size, Vector2 velocity)
            : base(name, hp, position, size, velocity)
        {
            this.name = name;
            position = Vector2.Zero;
            

        }


        /// <summary>
        /// 抽象初期化メソッド
        /// </summary>
        public abstract void Initialize();


        /// <summary>
        /// 抽象更新メソッド
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);



        /// <summary>
        /// 位置の受け渡し
        /// 引数で渡された、変数に自分の位置を渡す
        /// </summary>
        /// <param name="other"></param>
        //public void SetPosition(ref Vector2 other)
        //{
        //    other = position;
        //}

        //public void Draw(Renderer renderer)
        //{
        //    renderer.DrawTexture(name, position);
        //}



    }
}
