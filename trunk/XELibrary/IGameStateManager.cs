using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XELibrary
{
    public interface IGameStateManager
    {
        event EventHandler OnStateChange;
        GameState State { get; }
        void PopState();
        void PushState(GameState state);
        bool ContainsState(GameState state);
        void ChangeState(GameState newState);
    }
}
