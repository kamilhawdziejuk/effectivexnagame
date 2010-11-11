using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XELibrary
{
    public class Utility
    {
        public static Rectangle GetTitleSafeArea(GraphicsDevice _device, float percent)
        {
            Rectangle retval = new Rectangle(_device.Viewport.X,
            _device.Viewport.Y,
            _device.Viewport.Width,
            _device.Viewport.Height);

            return retval;
        }
    }
}
