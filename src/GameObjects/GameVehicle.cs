using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameXna.GameObjects
{
    public class GameVehicle : GameObject
    {
        private List<Bullet> bullets = new List<Bullet>();

        public List<Bullet> Bullets
        {
            get
            {
                return this.bullets;
            }
        }

        public GameVehicle(Game _game, Model _model, Matrix _world, string _name) : base(_game, _model, _world, _name)
        {
            for (int i = 0; i < 4; ++i)
            {
                var bulletWorld = Matrix.CreateScale(0.0005f) * Matrix.CreateTranslation(new Vector3(0.5f, i* 0.5f, 0.5f));
                var bullet = new Bullet(_game, null, bulletWorld, "bullet" + i.ToString());
                bullet.camera = this.camera;
                this.bullets.Add(bullet);
            }
        }

        public override void Draw()
        {
            base.Draw();

            foreach (Bullet b in this.bullets)
            {
                b.Draw();
            }
        }
    }
}
