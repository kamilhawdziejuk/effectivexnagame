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
                Vector2 pos = new Vector2(TitleSafeArea.Left, TitleSafeArea.Top);
                SpriteBatch sprite = new SpriteBatch(this.Game.GraphicsDevice);
                sprite.Begin();
                sprite.Draw(OurGame.TextureIntro, pos, Color.White);

                sprite.DrawString(OurGame.Font, "GameXNA - wersja alfa (2010)", pos, Color.Yellow);

                sprite.DrawString(OurGame.Font, "autor: Kamil Hawdziejuk", pos + new Vector2(0, 20), Color.Silver);

                 sprite.DrawString(OurGame.Font, "wcisnij S...", pos + new Vector2(0, 100), Color.Silver);

                sprite.End();

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

