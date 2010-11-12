using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XELibrary;
using Microsoft.Xna.Framework.Graphics;

namespace GameXna
{
    public interface ITitleIntroState : IGameState { }
    public interface IStartMenuState : IGameState { }
    public interface IOptionsMenuState : IGameState { }
    public interface IPlayingState : IGameState { }
    public interface IPausedState : IGameState { }
    public interface ILostGameState : IGameState { }
    public interface IWonGameState : IGameState
    {
        GameObject Winner
        {
            get;
            set;
        }
    }
    public interface IStartLevelState : IGameState { }
    public interface IYesNoDialogState : IGameState { }

    public interface IFadingState : IGameState
    {
        Color Color { get; set; }
    }
}
