using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XELibrary;

namespace GameXna.GameObjects
{
    public class GameVehicle : GameObject
    {
        private List<Bullet> bullets = new List<Bullet>();
        private FirstPersonCamera firstPersonCamera;

        public List<Bullet> Bullets
        {
            get
            {
                return this.bullets;
            }
        }

        public GameVehicle(Game _game, Model _model, Matrix _world, string _name) : base(_game, _model, _world, _name)
        {
            for (int i = 0; i < 100; ++i)
            {
                var bulletWorld = Matrix.CreateScale(0.0005f) *Matrix.CreateTranslation(new Vector3(0.5f, 0.5f, 0.5f));
                var bullet = new Bullet(_game, null, bulletWorld, "bullet" + i.ToString());
                bullet.Position = this.Position;
                this.bullets.Add(bullet);
            }
            this.firstPersonCamera = _game.Components[2] as FirstPersonCamera;
        }

        /// <summary>
        /// Aktualizuje stan obiektu
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //poruszam już wystrzelonymi pociskami
            foreach (Bullet fire in this.bullets.FindAll(a => a.State == BulletState.Running))
            {
                fire.Position += fire.TargetDirection;
                if (fire.Time++ > 2)
                {
                    fire.IsVisible = true;
                }
            }

            //odpalam nowy pocisk, po wciśnięciu lewego przycisku myszy
            if (this.input.MouseState.LeftButton == ButtonState.Pressed || this.input.KeyboardState.IsKeyDown(Keys.Space))
            {
                SoundManager sound = (this.Game.Components[2] as SoundManager);
                sound.StopAll();
                sound.Play("fire");

                Vector2 targetOnScreen = new Vector2((float)this.input.MouseState.X - this.Game.GraphicsDevice.ScissorRectangle.Center.X,
                    this.Game.GraphicsDevice.ScissorRectangle.Center.Y - (float)this.input.MouseState.Y);
                Vector3 clickedTarget = new Vector3(this.Scale * targetOnScreen.X, this.Scale * targetOnScreen.Y, 1.9f);

                var fired = this.bullets.Find(a => a.State == BulletState.Prepared);
                if (fired != null)
                {
                    fired.State = BulletState.Running;
                    fired.Position2 = this.Position2;
                    fired.TargetDirection = clickedTarget.Z * this.World0.Forward +clickedTarget.X * this.World0.Right + clickedTarget.Y * this.World0.Up;
                    fired.TargetDirection *= 0.05f;
                    fired.World.Forward = this.Scale * fired.TargetDirection;
                }
            }

            foreach (Bullet fire in this.bullets.FindAll(a => a.State == BulletState.Running))
            {
                fire.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw()
        {
            base.Draw();

            foreach (Bullet b in this.bullets)
            {
                if (b.State != BulletState.Prepared)
                {
                    b.Draw();
                }
            }
        }
    }
}
