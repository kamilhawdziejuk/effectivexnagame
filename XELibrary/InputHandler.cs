using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XELibrary
{
    public class InputHandler : GameComponent, IInputHandler
    {
        #region --- States ---

        private KeyboardState keyboardState;
#if !XBOX360
        private MouseState mouseState;
        private MouseState prevMouseState;
#endif

        #endregion

        #region --- Constructing & destroying objects ---

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public InputHandler(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IInputHandler), this);
#if !XBOX360
            Game.IsMouseVisible = true;
            prevMouseState = Mouse.GetState();
#endif
        }

        #endregion

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Game.Exit();
            }
#if !XBOX360
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            
#endif
            base.Update(gameTime);
        }

        #region IInputHandler Members

        public KeyboardState KeyboardState
        {
            get { return (keyboardState); }
        }

#if !XBOX360
        public MouseState MouseState
        {
            get { return (mouseState); }
        }
        public MouseState PreviousMouseState
        {
            get { return (prevMouseState); }
        }
#endif
        #endregion
    }
}
