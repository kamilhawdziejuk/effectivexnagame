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
        private Texture2D texture;

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
                //got here from our playing state,just pop myself off the stack
              /*  if (GameManager.ContainsState(OurGame.PlayingState.Value))
                    GameManager.PopState();
                else //starting game, queue first level
                    GameManager.ChangeState(OurGame.StartLevelState.Value);*/


                GameManager.PushState(OurGame.StartLevelState.Value);
            }

            //options menu
           // if (Input.WasPressed(0, Keys.O))
              //  GameManager.PushState(OurGame.OptionsMenuState.Value);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
           // return;
            if ((this.Game as GameXna).GameStateManager.State is StartMenuState)
            {
                Vector2 pos = new Vector2(TitleSafeArea.Left + 10, TitleSafeArea.Top + 30);

                OurGame.SpriteBatch.Begin();
                OurGame.SpriteBatch.Draw(OurGame.TextureIntro, pos, Color.White);
                OurGame.SpriteBatch.DrawString(OurGame.Font, "Wcisnij ENTER, aby rozpoczac...", pos, Color.Snow);
                OurGame.SpriteBatch.DrawString(OurGame.Font, "Wcisnij ESC, aby powrocic do tego okna...", pos + new Vector2(0, 20), Color.Snow);

                OurGame.SpriteBatch.DrawString(OurGame.Font, "STEROWANIE", pos + new Vector2(0, 100), Color.Silver);
                OurGame.SpriteBatch.DrawString(OurGame.Font, "Poruszanie: Strzalki", pos + new Vector2(0, 120), Color.Silver);
                OurGame.SpriteBatch.DrawString(OurGame.Font, " Obracanie: W/S/A/D lub myszka", pos + new Vector2(0, 140), Color.Silver);
                OurGame.SpriteBatch.DrawString(OurGame.Font, "Strzelanie: Spacja lub lewy przycisk myszki", pos + new Vector2(0, 160), Color.Silver);

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

        protected override void LoadGraphicsContent(bool loadAllContent)
        {
            //texture = Content.Load<Texture2D>(@”Content\Textures\startMenu”);
        }
    
        protected override void UnloadGraphicsContent(bool unloadAllContent)
        {
            if (unloadAllContent)
            {
             texture = null;
            }
            base.UnloadGraphicsContent(unloadAllContent);
        }
    }
}
