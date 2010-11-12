using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XELibrary;
using Microsoft.Xna.Framework.Input;

namespace GameXna
{
    public sealed class StartMenuState : BaseGameState, IStartMenuState
    {

        public StartMenuState(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IStartMenuState), this);
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.WasPressed(0, Keys.Escape))
            {
                //go back to title / intro screen
               // GameManager.ChangeState(OurGame.TitleIntroState.Value);
            }

            if (Input.WasPressed(0, Keys.Enter))
            {
                GameManager.PushState(OurGame.StartLevelState.Value);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
           // return;
            if ((this.Game as GameXna).GameStateManager.State is StartMenuState)
            {
                Vector2 pos = new Vector2(TitleSafeArea.Left, TitleSafeArea.Top);

                OurGame.SpriteBatch.Begin();
                OurGame.SpriteBatch.Draw(OurGame.TextureIntro, pos, Color.White);
    
                OurGame.SpriteBatch.DrawString(OurGame.Font, "STEROWANIE:", pos + new Vector2(100, 90), Color.Snow);
                OurGame.SpriteBatch.DrawString(OurGame.Font, "Obracanie:  Strzalki", pos + new Vector2(100, 120), Color.Silver);
                OurGame.SpriteBatch.DrawString(OurGame.Font, "Poruszanie: W/S/A/D", pos + new Vector2(100, 140), Color.Silver);
                OurGame.SpriteBatch.DrawString(OurGame.Font, "Celowanie:  Kursor myszki", pos + new Vector2(100, 160), Color.Silver);
                OurGame.SpriteBatch.DrawString(OurGame.Font, "Strzelanie: Spacja lub lewy przycisk myszki", pos + new Vector2(100, 180), Color.Silver);

                OurGame.SpriteBatch.DrawString(OurGame.Font, "Wcisnij ENTER, aby rozpoczac...", pos + new Vector2(100,400), Color.Snow);
                OurGame.SpriteBatch.DrawString(OurGame.Font, "Wcisnij ESC, aby powrocic do tego okna...", pos + new Vector2(100, 420), Color.Snow);

                OurGame.SpriteBatch.End();

                base.Draw(gameTime);
            }
        }

        public override void StateChanged(object sender, EventArgs e)
        {
            base.StateChanged(sender, e);
            if (GameManager.State != this.Value)
                Visible = true;
        }
    }
}
