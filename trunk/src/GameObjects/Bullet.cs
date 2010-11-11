using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameXna
{
    public class Bullet : GameObject
    {
        public BulletState State = BulletState.Prepared;
        private bool justFired = true;
        public Vector3 TargetDirection = Vector3.Zero;
        public int Time = 0;

        public Bullet(Game _game, Model _model, Matrix _world, string _name) : base(_game, _model, _world, _name)
        {
            this.IsVisible = true;
        }

        public override void Draw()
        {
            if (this.State != BulletState.Prepared && this.IsVisible)
            {
                base.Draw();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (this.State != BulletState.Prepared && justFired)
            {
                justFired = false;
            }

            base.Update(gameTime);
        }

    }

    /// <summary>
    /// Stan pocisku
    /// </summary>
    public enum BulletState
    {
        /// <summary>
        /// Przygotowany
        /// </summary>
        Prepared,
        /// <summary>
        /// Odpalony
        /// </summary>
        Running,
        /// <summary>
        /// Po trafieniu
        /// </summary>
        Hit,
        /// <summary>
        /// Poza planszą
        /// </summary>
        Outside
    }
}
