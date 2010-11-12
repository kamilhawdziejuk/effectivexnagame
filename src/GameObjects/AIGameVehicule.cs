//12.11.2010
//Kamil Hawdziejuk

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameXna.GameObjects
{
    public class AIGameVehicule : GameVehicle, IAIGameObject
    {
        public bool AIActivated = true;

        public AIGameVehicule(Game _game, Model _model, Matrix _world, string _name) : base(_game, _model, _world, _name)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (this.AIActivated)
                this.Interact();
            base.Update(gameTime);
        }

        #region IAIGameObject Members

        /// <summary>
        /// AI Move
        /// </summary>
        /// <returns></returns>
        public bool Interact()
        {
            Random rand = new Random();
            //losowy obrót
            float rotation = 2* (float)rand.NextDouble();
            Matrix yaw = Matrix.CreateRotationY(rand.Next(-2, 2) * MathHelper.ToRadians(rotation));
            if (rand.Next(0, 2)!= 0)
            {
               this.Rotation += rotation;
               this.World *= yaw;
            }
            //losowa pozycja
            this.Position = this.Position + this.World0.Forward * (float)rand.NextDouble()/40 ;

            //strzał w kierunku wroga
            GameObject enemy = this.OurGame.ObjectsManager.GameObjects.Find(a => (a is GameVehicle) && a != this);
            Bullet preperedBullet = this.Bullets.Find(a => a.State == BulletState.Prepared);

            if (rand.Next(-20,20) == 0)
            {
                preperedBullet.State = BulletState.Running;
                preperedBullet.Position2 = this.Position2;
                preperedBullet.TargetDirection = enemy.Position2;
                preperedBullet.TargetDirection = new Vector3(preperedBullet.TargetDirection.X * 0.010f, 
                preperedBullet.TargetDirection.Y * 0.04f, preperedBullet.TargetDirection.Z * 0.010f);
                preperedBullet.World.Forward = this.Scale * preperedBullet.TargetDirection;
            }
            return true;
        }

        #endregion
    }
}
