using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameXna
{
    public sealed class TitleIntroState : BaseGameState, ITitleIntroState
    {
        private Texture2D texture;
    
        public TitleIntroState(Game game)
        : base(game)
        {
            game.Services.AddService(typeof(ITitleIntroState), this);
        }

        public override void Update(GameTime gameTime)
        {
            
            if (Input.WasPressed(0, Keys.Escape))
            {
                //OurGame.Exit();
            }
            if (Input.WasPressed(0, Keys.S))
            {
                // push our start menu onto the stack
                GameManager.PushState(OurGame.StartMenuState.Value);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if ((this.Game as GameXna).GameStateManager.State is TitleIntroState)
            {
                string gameDescription = "\"WEHIKULOWE STARCIE\", to gra komputerowa, odzwierciedlajaca \n " +
                    "pojedynek wehikulow na arenie. Sterujesz jednym z dwoch \n dostepnych wehikulow: " +
                    "helikopterem badz samochodem. \n Celem gry jest trafienie przeciwnika, nie dajac sie \n jednoczesnie samemu zestrzelic...";


                Vector2 pos = new Vector2(TitleSafeArea.Left, TitleSafeArea.Top);
                OurGame.SpriteBatch.Begin();
                OurGame.SpriteBatch.Draw(OurGame.TextureIntro, pos, Color.White);
                OurGame.SpriteBatch.DrawString(OurGame.Font, "WEHIKULOWE STARCIE - wersja alfa (2010)", pos + new Vector2(100,10), Color.WhiteSmoke);
                OurGame.SpriteBatch.DrawString(OurGame.Font, "autor: Kamil Hawdziejuk", pos + new Vector2(100, 30), Color.WhiteSmoke);


                OurGame.SpriteBatch.DrawString(OurGame.Font, gameDescription, pos+new Vector2(100,100), Color.Silver);
                
                OurGame.SpriteBatch.DrawString(OurGame.Font, "Wcisnij \"S\", aby przejsc do opcji.", pos + new Vector2(100, 400), Color.Silver);
                OurGame.SpriteBatch.End();
                base.Draw(gameTime);
            }
        }

        protected override void LoadGraphicsContent(bool loadAllContent)
        {
           // texture = Content.Load<Texture2D>("@Content\\Textures\\zachod_slonca");
        }

        protected override void UnloadGraphicsContent(bool unloadAllContent)
        {
            if (unloadAllContent)
                texture = null;
            base.UnloadGraphicsContent(unloadAllContent);
        }
}
}

