using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameXna.GameStates
{
    public sealed class StartLevelState : BaseGameState, IStartLevelState
    {
        private bool demoMode = true;
        private bool displayedDemoDialog = false;
        private DateTime levelLoadTime;
        private readonly int loadSoundTime = 2500;
        private string levelText = "LEVEL";
        private string currentLevel;
        private Vector2 levelTextPosition;
        private Vector2 levelTextShadowPosition;
        private Vector2 levelNumberPosition;
        private Vector2 levelNumberShadowPosition;

        public StartLevelState(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IStartLevelState), this);
        }

        public override void Update(GameTime gameTime)
        {

            if (Input.WasPressed(0, Keys.Escape))
            {
                // push our start menu onto the stack
                GameManager.PopState();
            }

            base.Update(gameTime);
        }

        public override void StateChanged(object sender, EventArgs e)
        {
            base.StateChanged(sender, e);
            bool startingLevel = true;
            if (GameManager.State == this.Value)
            {
               // if (demoMode && !displayedDemoDialog)
                {
                    //We could set properties on our YesNoDialog
                    //so it could have a custom message and custom
                    //Yes / No buttons ...
                    //YesNoDialogState.YesCaption = “Of course!”;
                    //GameManager.PushState(OurGame.YesNoDialogState.Value);
                    this.Visible = true;
                    displayedDemoDialog = true;
                    startingLevel = false;
                }
            }

            if (startingLevel)
            {
                //play sound
                levelLoadTime = DateTime.Now;
                //currentLevel = (OurGame.PlayingState.CurrentLevel + 1).ToString();
                Vector2 viewport = new Vector2(GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height);
                Vector2 levelTextLength = OurGame.Font.MeasureString(levelText);
                Vector2 levelNumberLength = OurGame.Font.MeasureString(currentLevel);

                // levelTextShadowPosition = (viewport – levelTextLength * 3) / 2;
                // levelNumberShadowPosition = (viewport – levelNumberLength * 3) / 2;
                levelNumberShadowPosition.Y += OurGame.Font.LineSpacing * 3;

                levelTextPosition.X = levelTextShadowPosition.X + 2;
                levelTextPosition.Y = levelTextShadowPosition.Y + 2;
                levelNumberPosition.X = levelNumberShadowPosition.X + 2;
                levelNumberPosition.Y = levelNumberShadowPosition.Y + 2;
            }
        }
     }
  }

