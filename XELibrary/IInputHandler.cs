//Kamil Hawdziejuk
//24-07-2010

using Microsoft.Xna.Framework.Input;

namespace XELibrary
{
    public interface IInputHandler
    {
        KeyboardState KeyboardState { get; }
#if !XBOX360
        MouseState MouseState { get; }
        MouseState PreviousMouseState { get; }
#endif
    }
}
