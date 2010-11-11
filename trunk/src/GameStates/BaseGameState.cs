using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using XELibrary;

namespace GameXna
{
    public class BaseGameState : GameState
    {        
        protected GameXna OurGame;
        protected ContentManager Content;

        protected IInputHandler Input;

        public BaseGameState(Game game)
            : base(game)
        {
            Content = new ContentManager(game.Services);
            OurGame = (GameXna)game;
            Input = (IInputHandler)game.Services.GetService(typeof(IInputHandler));
        }
    }
}
