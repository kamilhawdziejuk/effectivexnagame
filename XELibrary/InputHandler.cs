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
        public InputHandler(Game game)
            : base(game)
        {
        }
    }
}
