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

        public Bullet(Game _game, Model _model, Matrix _world, string _name) : base(_game, _model, _world, _name)
        {
        }

        public override void Draw()
        {
            if (this.State != BulletState.Prepared)
            {
                base.Draw();
            }
        }
        
    }

    public enum BulletState
    {
        Prepared,
        Started,
        Hit,
        Outside
    }
}
