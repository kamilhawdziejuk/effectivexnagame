using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GameXna
{
    public sealed class WonGameState : BaseGameState, IWonGameState
    {
        GameObject winner = null;

        public WonGameState(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IWonGameState), this);
        }

        public GameObject Winner
        {
            get
            {
                return this.winner;
            }
            set
            {
                this.winner = value;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.WasPressed(0, Keys.Escape))
            {
                this.Game.Exit();
            }
            else if (Input.WasPressed(0, Keys.S))
            {
                GameManager.PopState();
                OurGame.ReStart();
            }

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            if ((this.Game as GameXna).GameStateManager.State is IWonGameState)
            {
                Vector2 pos = new Vector2(TitleSafeArea.Left, TitleSafeArea.Top);

                Color resultColor = Color.Green;
                string gameOverMessage = "WYGRALES!";
                if (this.OurGame.ObjectsManager.ActiveObject != this.Winner)
                {
                    resultColor = Color.Red;
                    gameOverMessage = "PRZEGRALES";
                }

                OurGame.SpriteBatch.Begin();
                OurGame.SpriteBatch.Draw(OurGame.TextureIntro, pos, Color.White);

                OurGame.SpriteBatch.DrawString(OurGame.Font, "KONIEC GRY:", pos + new Vector2(100, 90), Color.Snow);
                OurGame.SpriteBatch.DrawString(OurGame.Font, gameOverMessage, pos + new Vector2(100, 110), resultColor);
           
                OurGame.SpriteBatch.DrawString(OurGame.Font, "Wcisnij ESC, aby ja opuscic", pos + new Vector2(100, 420), Color.Snow);
                OurGame.SpriteBatch.DrawString(OurGame.Font, "Wcisnij \"S\", aby zaczac od nowa", pos + new Vector2(100, 440), Color.Snow);


                OurGame.SpriteBatch.End();

                base.Draw(gameTime);
            }
        }

    }
}
