using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XELibrary
{
    public class FirstPersonCamera : Camera
    {
        public FirstPersonCamera(Game game)
            : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (input.KeyboardState.IsKeyDown(Keys.A))
            {
                movement.X--;
            }
            if (input.KeyboardState.IsKeyDown(Keys.D))
            {
                movement.X++;
            }
            if (input.KeyboardState.IsKeyDown(Keys.S))
            {
                movement.Z++;
            }
            if (input.KeyboardState.IsKeyDown(Keys.W))
            {
                movement.Z--;
            }
            //make sure we don’t increase speed if pushing up and over (diagonal)
            if (movement.LengthSquared() != 0)
            {
                movement.Normalize();
            }
            //update movement (none for this base class)

            base.Update(gameTime);
        }
    }
}
