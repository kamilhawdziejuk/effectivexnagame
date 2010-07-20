using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XELibrary
{
    public class InputHandler : GameComponent, IInputHandler
    {
        private KeyboardState keyboardState;

        public InputHandler(Game game)
            : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Game.Exit();
            }
            base.Update(gameTime);
        }

        #region IInputHandler Members

        public KeyboardState KeyboardState
        {
            get { return (keyboardState); }
        }

        #endregion
    }
}
