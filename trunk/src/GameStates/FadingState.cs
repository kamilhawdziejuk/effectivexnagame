﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameXna.GameStates
{
    public sealed class FadingState : BaseGameState, IFadingState
    {
        private Texture2D fadeTexture;
        private float fadeAmount;
        private double fadeStartTime;
        private Color color;

        public Color Color
        {
            get { return (color); }
            set { color = value; }
        }

        public FadingState(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IFadingState), this);
        }

        public override void Update(GameTime gameTime)
        {
            if (fadeStartTime == 0)
                fadeStartTime = gameTime.TotalGameTime.TotalMilliseconds;

            fadeAmount += (.25f * (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (gameTime.TotalGameTime.TotalMilliseconds > fadeStartTime + 4000)
            {
                //Once we are done fading, change back to title screen.
                GameManager.ChangeState(OurGame.TitleIntroState.Value);
            }

        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.RenderState.SourceBlend = Blend.SourceAlpha;
            GraphicsDevice.RenderState.DestinationBlend = Blend.InverseSourceAlpha;
            Vector4 fadeColor = color.ToVector4();
            fadeColor.W = fadeAmount; //set transparancy
            OurGame.SpriteBatch.Draw(fadeTexture, Vector2.Zero,
            new Color(fadeColor));
            base.Draw(gameTime);
        }

        public override void StateChanged(object sender, EventArgs e)
        {
            //Set up our initial fading values
            if (GameManager.State == this.Value)
            {
                fadeAmount = 0;
                fadeStartTime = 0;
            }
        }
    }    
}
