using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XELibrary
{
    public class GameStateManager : GameComponent, IGameStateManager
    {
        private Stack<GameState> states = new Stack<GameState>();
        public event EventHandler OnStateChange;
        private int initialDrawOrder = 1000;
        private int drawOrder;

        public GameStateManager(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IGameStateManager), this);
            drawOrder = initialDrawOrder;
        }

        public GameState State
        {
            get { return (states.Peek()); }
        }

        public void PopState()
        {
            RemoveState();
            drawOrder -= 100;
            //Let everyone know we just changed states
            if (OnStateChange != null)
                OnStateChange(this, null);
        }

        private void RemoveState()
        {
            GameState oldState = (GameState)states.Peek();
            //Unregister the event for this state
            OnStateChange -= oldState.StateChanged;
            //remove the state from our game components
            Game.Components.Remove(oldState.Value);
            states.Pop();
        }

        public void PushState(GameState newState)
        {
            drawOrder += 100;
            newState.DrawOrder = drawOrder;
            AddState(newState);
            //Let everyone know we just changed states
            if (OnStateChange != null)
                OnStateChange(this, null);
        }

        private void AddState(GameState state)
        {
            
            if (!this.ContainsState(state))
            {
                Game.Components.Add(state);
            }
            if (!states.Contains(state))
            {
                states.Push(state);
            }
                //Register the event for this state
                OnStateChange += state.StateChanged;
            
        }

        public void ChangeState(GameState newState)
        {
            //We are changing states, so pop everything ...
            //if we don’t want to really change states but just modify,
            //we should call PushState and PopState
            while (states.Count > 0)
                RemoveState();
            //changing state, reset our draw order
            newState.DrawOrder = drawOrder = initialDrawOrder;
            AddState(newState);
            //Let everyone know we just changed states
            if (OnStateChange != null)
                OnStateChange(this, null);
        }

        public bool ContainsState(GameState state)
        {
            return (states.Contains(state));
        }
    }
}
