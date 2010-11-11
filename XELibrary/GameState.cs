using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XELibrary
{
    public class GameState : DrawableGameComponent, IGameState
    {
        #region IGameState Members

        public GameState Value
        {
            get { return (this); }
        }

        #endregion

        protected IGameStateManager GameManager;
        protected IInputHandler Input;
        protected Rectangle TitleSafeArea;

        public GameState(Game game)
        : base(game)
        {
            GameManager = (IGameStateManager)game.Services.GetService(
            typeof(IGameStateManager));
            Input = (IInputHandler)game.Services.GetService(
            typeof(IInputHandler));
        }
        protected override void LoadGraphicsContent(bool loadAllContent)
        {
            if (loadAllContent)
            {
                TitleSafeArea = Utility.GetTitleSafeArea(GraphicsDevice, 0.85f);
            }
            base.LoadGraphicsContent(loadAllContent);
        }

        public virtual void StateChanged(object sender, EventArgs e)
        {
            if (GameManager.State == this.Value)
                Visible = Enabled = true;
            else
                Visible = Enabled = false;
        }

    }
}
